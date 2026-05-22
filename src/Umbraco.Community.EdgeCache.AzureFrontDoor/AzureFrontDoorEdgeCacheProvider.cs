using Azure;
using Azure.ResourceManager.Cdn.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

namespace Umbraco.Community.EdgeCache.AzureFrontDoor;

public class AzureFrontDoorEdgeCacheProvider
{
    private readonly IOptionsMonitor<AzureFrontDoorOptions> _options;
    private readonly IAzureFrontDoorClientFactory _clientFactory;
    private readonly ILogger<AzureFrontDoorEdgeCacheProvider> _logger;

    public AzureFrontDoorEdgeCacheProvider(
        IOptionsMonitor<AzureFrontDoorOptions> options,
        IAzureFrontDoorClientFactory clientFactory,
        ILogger<AzureFrontDoorEdgeCacheProvider> logger)
    {
        _options = options;
        _clientFactory = clientFactory;
        _logger = logger;
    }

    public Task<PurgeResult> PurgeAsync(
        IEnumerable<string> contentPaths,
        IEnumerable<string>? domains = null,
        CancellationToken cancellationToken = default)
    {
        var instances = _options.CurrentValue.Instances;
        return PurgeManyAsync(instances, contentPaths, domains, cancellationToken);
    }

    public Task<PurgeResult> PurgeAsync(
        string instanceName,
        IEnumerable<string> contentPaths,
        IEnumerable<string>? domains = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(instanceName);

        var instance = _options.CurrentValue.Instances
            .FirstOrDefault(i => string.Equals(i.Name, instanceName, StringComparison.OrdinalIgnoreCase));

        if (instance is null)
        {
            _logger.LogWarning("Azure Front Door instance '{InstanceName}' is not configured; skipping purge.", instanceName);
            return Task.FromResult(new PurgeResult(Array.Empty<InstancePurgeResult>()));
        }

        return PurgeManyAsync(new[] { instance }, contentPaths, domains, cancellationToken);
    }

    private async Task<PurgeResult> PurgeManyAsync(
        IList<AzureFrontDoorInstanceOptions> instances,
        IEnumerable<string> contentPaths,
        IEnumerable<string>? domains,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(contentPaths);

        var paths = contentPaths.ToList();
        if (paths.Count == 0)
        {
            throw new ArgumentException("At least one content path must be supplied.", nameof(contentPaths));
        }

        if (instances.Count == 0)
        {
            _logger.LogDebug("No Azure Front Door instances configured; purge is a no-op.");
            return new PurgeResult(Array.Empty<InstancePurgeResult>());
        }

        var domainOverride = domains?.ToList();

        var tasks = instances
            .Select(instance => PurgeOneAsync(instance, paths, domainOverride, cancellationToken))
            .ToArray();

        var results = await Task.WhenAll(tasks);
        return new PurgeResult(results);
    }

    private async Task<InstancePurgeResult> PurgeOneAsync(
        AzureFrontDoorInstanceOptions instance,
        IList<string> paths,
        IList<string>? domainOverride,
        CancellationToken cancellationToken)
    {
        try
        {
            var endpoint = _clientFactory.GetEndpoint(instance);

            var content = new FrontDoorPurgeContent(paths);
            var domainsForRequest = domainOverride ?? instance.Domains;
            if (domainsForRequest is not null)
            {
                foreach (var domain in domainsForRequest)
                {
                    content.Domains.Add(domain);
                }
            }

            var operation = await endpoint.PurgeContentAsync(WaitUntil.Started, content, cancellationToken);

            _logger.LogInformation(
                "Started Azure Front Door purge for instance '{InstanceName}' (operation {OperationId}).",
                instance.Name,
                operation.Id);

            return new InstancePurgeResult(instance.Name, Success: true, OperationId: operation.Id, Error: null);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Azure Front Door purge failed for instance '{InstanceName}'.", instance.Name);
            return new InstancePurgeResult(instance.Name, Success: false, OperationId: null, Error: ex.Message);
        }
    }
}

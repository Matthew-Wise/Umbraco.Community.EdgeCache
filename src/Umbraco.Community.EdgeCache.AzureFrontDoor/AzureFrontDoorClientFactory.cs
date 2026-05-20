using System.Collections.Concurrent;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Cdn;
using Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

namespace Umbraco.Community.EdgeCache.AzureFrontDoor;

public class AzureFrontDoorClientFactory : IAzureFrontDoorClientFactory
{
    private readonly ConcurrentDictionary<string, ArmClient> _clients = new(StringComparer.Ordinal);

    public FrontDoorEndpointResource GetEndpoint(AzureFrontDoorInstanceOptions instance)
    {
        ArgumentNullException.ThrowIfNull(instance);

        var client = _clients.GetOrAdd(GetCredentialKey(instance.Credential), _ => new ArmClient(BuildCredential(instance.Credential)));

        var id = FrontDoorEndpointResource.CreateResourceIdentifier(
            instance.SubscriptionId,
            instance.ResourceGroup,
            instance.ProfileName,
            instance.EndpointName);

        return client.GetFrontDoorEndpointResource(id);
    }

    private static TokenCredential BuildCredential(AzureFrontDoorCredentialOptions? credential)
    {
        if (credential is { IsServicePrincipal: true })
        {
            return new ClientSecretCredential(credential.TenantId, credential.ClientId, credential.ClientSecret);
        }

        return new DefaultAzureCredential();
    }

    private static string GetCredentialKey(AzureFrontDoorCredentialOptions? credential)
    {
        if (credential is { IsServicePrincipal: true })
        {
            return $"sp:{credential.TenantId}:{credential.ClientId}";
        }

        return "default";
    }
}

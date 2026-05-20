using Microsoft.Extensions.Options;

namespace Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

public class AzureFrontDoorOptionsValidator : IValidateOptions<AzureFrontDoorOptions>
{
    public ValidateOptionsResult Validate(string? name, AzureFrontDoorOptions options)
    {
        if (options.Instances.Count == 0)
        {
            // No configured instances is a valid state — the provider just becomes a no-op.
            // Skip validation in that case so consumers that haven't wired up AFD yet don't fail startup.
            return ValidateOptionsResult.Success;
        }

        var failures = new List<string>();
        var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var i = 0; i < options.Instances.Count; i++)
        {
            var instance = options.Instances[i];
            var label = string.IsNullOrWhiteSpace(instance.Name) ? $"Instances[{i}]" : $"Instances[{i}] '{instance.Name}'";

            if (string.IsNullOrWhiteSpace(instance.Name))
            {
                failures.Add($"{label}: Name is required.");
            }
            else if (!seenNames.Add(instance.Name))
            {
                failures.Add($"{label}: Name must be unique within Instances.");
            }

            if (string.IsNullOrWhiteSpace(instance.SubscriptionId))
            {
                failures.Add($"{label}: SubscriptionId is required.");
            }

            if (string.IsNullOrWhiteSpace(instance.ResourceGroup))
            {
                failures.Add($"{label}: ResourceGroup is required.");
            }

            if (string.IsNullOrWhiteSpace(instance.ProfileName))
            {
                failures.Add($"{label}: ProfileName is required.");
            }

            if (string.IsNullOrWhiteSpace(instance.EndpointName))
            {
                failures.Add($"{label}: EndpointName is required.");
            }
        }

        return failures.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(failures);
    }
}

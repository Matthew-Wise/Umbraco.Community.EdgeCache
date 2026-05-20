namespace Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

public class AzureFrontDoorInstanceOptions
{
    public string Name { get; set; } = string.Empty;

    public string SubscriptionId { get; set; } = string.Empty;

    public string ResourceGroup { get; set; } = string.Empty;

    public string ProfileName { get; set; } = string.Empty;

    public string EndpointName { get; set; } = string.Empty;

    public IList<string>? Domains { get; set; }

    public AzureFrontDoorCredentialOptions? Credential { get; set; }
}

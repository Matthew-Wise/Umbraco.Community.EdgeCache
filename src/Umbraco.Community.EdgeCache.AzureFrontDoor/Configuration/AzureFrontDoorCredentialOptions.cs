namespace Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

public class AzureFrontDoorCredentialOptions
{
    public string? TenantId { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public bool IsServicePrincipal =>
        !string.IsNullOrWhiteSpace(TenantId) &&
        !string.IsNullOrWhiteSpace(ClientId) &&
        !string.IsNullOrWhiteSpace(ClientSecret);
}

namespace Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

public class AzureFrontDoorOptions
{
    public const string SectionName = "EdgeCache:AzureFrontDoor";

    public IList<AzureFrontDoorInstanceOptions> Instances { get; set; } = new List<AzureFrontDoorInstanceOptions>();
}

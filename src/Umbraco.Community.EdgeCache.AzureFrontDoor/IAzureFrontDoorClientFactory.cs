using Azure.ResourceManager.Cdn;
using Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

namespace Umbraco.Community.EdgeCache.AzureFrontDoor;

public interface IAzureFrontDoorClientFactory
{
    FrontDoorEndpointResource GetEndpoint(AzureFrontDoorInstanceOptions instance);
}

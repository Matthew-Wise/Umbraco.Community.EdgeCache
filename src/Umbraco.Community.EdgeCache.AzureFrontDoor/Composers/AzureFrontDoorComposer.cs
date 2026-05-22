using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.EdgeCache.AzureFrontDoor.Configuration;

namespace Umbraco.Community.EdgeCache.AzureFrontDoor.Composers;

public class AzureFrontDoorComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services
            .AddOptions<AzureFrontDoorOptions>()
            .BindConfiguration(AzureFrontDoorOptions.SectionName)
            .ValidateOnStart();

        builder.Services.AddSingleton<IValidateOptions<AzureFrontDoorOptions>, AzureFrontDoorOptionsValidator>();

        builder.Services.AddSingleton<IAzureFrontDoorClientFactory, AzureFrontDoorClientFactory>();
        builder.Services.AddSingleton<AzureFrontDoorEdgeCacheProvider>();
    }
}

# Edge Cache

[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.EdgeCache?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.EdgeCache/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.EdgeCache?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.EdgeCache)
[![GitHub license](https://img.shields.io/github/license/Matthew-Wise/Umbraco.Community.EdgeCache?color=8AB803)](../LICENSE)

Edge Cache is an Umbraco package that purges CDN / edge caches when Umbraco content changes. The core package defines the cache-purging abstractions and notification handlers, and ships with two optional provider packages:

- **Umbraco.Community.EdgeCache.CloudFlare** – purges Cloudflare's edge cache.
- **Umbraco.Community.EdgeCache.AzureFrontDoor** – purges Azure Front Door's edge cache.

## Installation

Add the core package to an existing Umbraco website (v17+) from NuGet:

```
dotnet add package Umbraco.Community.EdgeCache
```

Then add whichever provider(s) you need:

```
dotnet add package Umbraco.Community.EdgeCache.CloudFlare
dotnet add package Umbraco.Community.EdgeCache.AzureFrontDoor
```

## Versioning

Every package follows `<UmbracoMajor>.<PackageMajor>.<Patches>`, e.g. `17.0.0`. Each package releases independently from its own prefixed tag:

| Package | Tag prefix | Example |
|---|---|---|
| Core | `core-` | `core-17.0.0` |
| CloudFlare | `cloudflare-` | `cloudflare-17.0.0` |
| Azure Front Door | `azurefrontdoor-` | `azurefrontdoor-17.0.0` |

## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).

## Acknowledgments

TODO
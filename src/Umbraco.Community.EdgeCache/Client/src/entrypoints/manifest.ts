export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Edge CacheEntrypoint",
    alias: "EdgeCache.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];

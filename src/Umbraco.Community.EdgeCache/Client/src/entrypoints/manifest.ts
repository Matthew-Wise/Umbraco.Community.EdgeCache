export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Edge Cache Entrypoint",
    alias: "EdgeCache.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];

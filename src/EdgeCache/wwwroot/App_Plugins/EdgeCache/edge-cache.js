const a = [
  {
    name: "Edge CacheEntrypoint",
    alias: "EdgeCache.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint-CO1e898E.js")
  }
], e = [
  {
    name: "Edge CacheDashboard",
    alias: "EdgeCache.Dashboard",
    type: "dashboard",
    js: () => import("./dashboard.element-DxfChsYh.js"),
    meta: {
      label: "Example Dashboard",
      pathname: "example-dashboard"
    },
    conditions: [
      {
        alias: "Umb.Condition.SectionAlias",
        match: "Umb.Section.Content"
      }
    ]
  }
], t = [
  ...a,
  ...e
];
export {
  t as manifests
};
//# sourceMappingURL=edge-cache.js.map

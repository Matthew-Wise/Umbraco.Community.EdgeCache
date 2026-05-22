namespace Umbraco.Community.EdgeCache.AzureFrontDoor;

public sealed record InstancePurgeResult(string InstanceName, bool Success, string? OperationId, string? Error);

public sealed class PurgeResult
{
    public PurgeResult(IReadOnlyList<InstancePurgeResult> instances)
    {
        Instances = instances;
    }

    public IReadOnlyList<InstancePurgeResult> Instances { get; }

    public bool AllSucceeded => Instances.All(i => i.Success);

    public bool AnySucceeded => Instances.Any(i => i.Success);
}

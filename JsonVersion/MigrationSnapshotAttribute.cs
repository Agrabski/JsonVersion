namespace JsonVersion;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MigrationSnapshotAttribute : Attribute
{
	public MigrationSnapshotAttribute(Type type, int version)
	{
		Type = type;
		Version = version;
	}

	public Type Type { get; }
	public int Version { get; }
}

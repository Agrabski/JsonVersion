namespace JsonVersion;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
public sealed class MigrationAssemblyAttribute : Attribute
{
	public MigrationAssemblyAttribute(string assemblyName)
	{
		AssemblyName = assemblyName;
	}

	public string AssemblyName { get; }
}

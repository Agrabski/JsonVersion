using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonVersion;
public class JsonVersioningSerializer
{
	public string Serialize<T>(T obj)
	{
		var json = JsonSerializer.Serialize(obj);
		var version = GetVersion(obj);
		return $"{version}{json}";
	}

	public T Deserialize<T>(string json)
	{
		var versioned = JsonSerializer.Deserialize<VersionedJson>(json);
		var migrationAssembly = GetMigrationAssembly(typeof(T));
		var currentType = PickTypeForVersion(typeof(T), migrationAssembly, versioned.Version);
		var obj = JsonSerializer.Deserialize(versioned.Data, currentType);
		return Migrate<T>(obj, migrationAssembly, versioned.Version);
	}

	private T Migrate<T>(object? currentObject, Assembly migrationAssembly, int currentVersion)
	{
		var targetType = typeof(T);
		var snapshots = GetMigrationSnapshots<T>(migrationAssembly).ToDictionary((kv)=>kv.Version, kv=>kv.Type);
	}

	private IEnumerable<(Type Type, int Version)> GetMigrationSnapshots<T>(Assembly migrationAssembly)
	{
		foreach(var type in migrationAssembly.GetTypes())
			if(type.GetCustomAttribute<MigrationSnapshotAttribute>() is MigrationSnapshotAttribute attribute)
				yield return (type, attribute.Version);
	}

	private static Assembly GetMigrationAssembly(Type currentType)
	{
		if (currentType.GetCustomAttribute<MigrationAssemblyAttribute>() is MigrationAssemblyAttribute attribute)
			return Assembly.Load(attribute.AssemblyName);
		return currentType.Assembly;
	}
	private Type PickTypeForVersion(Type type, Assembly migrationAssembly, int version) => throw new NotImplementedException();
}

using System.Text.Json;

namespace JsonVersion;

public class VersionedJson
{
	public int Version { get; set; }
	public JsonElement Data { get; set; }
}


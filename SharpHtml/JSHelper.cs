using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SharpHtml;

public static class JSObjectExtensions
{
	public static string? ToJS(this object? obj)
	{
		if (obj == null) return null;

		string jsonText = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});
		string regexPattern = "\"([^\"]+)\":";
		string value = Regex.Replace(jsonText, regexPattern, "$1:");
		value = value.Replace("'", "\\'");
		value = value.Replace("\"", "'");

		return value;
	}

}


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		string regexPattern = "\"([^\"]+)\":"; // the "propertyName": pattern
		string value = Regex.Replace(jsonText, regexPattern, "$1:");
		value = value.Replace("'", "\\'");
		value = value.Replace("\"", "'");

		return value;
	}

}


using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpHtml;

public partial class HtmlAttributes
{
	protected Dictionary<string, string?> dict = new();

	public string? Text => Get();

	List<string> Singles = new()
	{
		"required",
		"open",
		//"disabled",
	};

	List<string> Specials = new()
	{
		"jsonVals",
	};

	string? Get()
	{
		string? val = null;

		foreach (var attr in dict)
		{
			if (Specials.Contains(attr.Key))
			{
				if (attr.Key == "jsonVals")
					val += @$" hx-vals='{attr.Value}' ";
			}
			else if (Singles.Contains(attr.Key))
				val += @$" {attr.Key}";
			else
				val += @$" {attr.Key}=""{attr.Value}""";
		};

		return val;
	}

	public string? placeholder
	{
		set { dict["placeholder"] = value; }
	}

	public string? className
	{
		set { dict["class"] = value; }
	}

	public string? hfor
	{
		set { dict["for"] = value; }
	}

	public string? id
	{
		set { dict["id"] = value; }
	}

	public string? required
	{
		set { dict["required"] = value; }
	}

	public string rows
	{
		set { dict["rows"] = value.ToString(); }
	}

	public string? htype
	{
		set { dict["type"] = value; }
	}

	public string? open
	{
		set { dict["open"] = value; }
	}

	public string? alt
	{
		set { dict["alt"] = value; }
	}

	public string? src
	{
		set { dict["src"] = value; }
	}

	public string? href
	{
		set { dict["href"] = value; }
	}

	public string? method
	{
		set { dict["method"] = value; }
	}
	public string? action
	{
		set { dict["action"] = value; }
	}

	public bool disabled
	{
		set { if (value) dict["disabled"] = ""; }
	}

	public (string key, string value) custData
	{
		set => dict["data-" + value.key] = value.value;
	}

	public (string key, string value) custData1
	{
		set => dict["data-" + value.key] = value.value;
	}

}


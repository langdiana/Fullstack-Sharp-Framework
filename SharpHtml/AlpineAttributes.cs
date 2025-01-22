namespace SharpHtml;

public partial class HtmlAttributes
{
	public string? xData
	{
		set { dict["x-data"] = value; }
	}

	public object xObject
	{
		set { dict["x-data"] = value.ToJS(); }
	}

	public string? xClick
	{
		set { dict["@click"] = value; }
	}

	public string? xKeyupEnter
	{
		set { dict["@keyup.enter"] = value; }
	}

	public string? xLoad
	{
		set { dict["@load"] = value; }
	}

	public string? xClickOutside
	{
		set { dict["@click.outside"] = value; }
	}

	public string? xPrevent
	{
		set { dict["@click.prevent"] = value; }
	}

	public string? xText
	{
		set { dict["x-text"] = value; }
	}

	public string? xHtml
	{
		set { dict["x-html"] = value; }
	}

	public string? xShow
	{
		set { dict["x-show"] = value; }
	}

	public string? xModel
	{
		set { dict["x-model"] = value; }
	}

	public string? xFor
	{
		set { dict["x-for"] = value; }
	}

	public string? xInit
	{
		set { dict["x-init"] = value; }
	}

	public string? xKey
	{
		set { dict[":key"] = value; }
	}

	public string? xIf
	{
		set { dict["x-if"] = value; }
	}

	public string? xRef
	{
		set { dict["x-ref"] = value; }
	}

	public string? xEffect
	{
		set { dict["x-effect"] = value; }
	}

	public (string key, string value) xBind
	{
		//set => dict["x-bind:" + value.key] = value.value;
		set => dict[":" + value.key] = value.value;
	}

	public (string key, string value) xBind1
	{
		set => dict[":" + value.key] = value.value;
	}

	public string? xTarget
	{
		set { dict["x-target"] = value; }
	}

	public string xBindRef
	{
		set
		{
			xBind = ("href", value);
		}
	}

	public string xBindSrc
	{
		set
		{
			xBind = ("src", value);
		}
	}

}

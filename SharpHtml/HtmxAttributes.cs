namespace SharpHtml;

public partial class HtmlAttributes
{
	public string? hxGet
	{
		set { dict["hx-get"] = value; }
	}

	public string? hxPost
	{
		set
		{
			dict["hx-post"] = value;
		}
	}

	public string? hxDelete
	{
		set { dict["hx-delete"] = value; }
	}

	public string? hxTarget
	{
		set { dict["hx-target"] = value; }
	}

	public string? hxSwap
	{
		set { dict["hx-swap"] = value; }
	}

	public bool hxOob
	{
		set { if (value) dict["hx-swap-oob"] = "true"; }
	}

	public string? hxVals
	{
		set { dict["hx-vals"] = value; }
	}

	public string? hxExt
	{
		set { dict["hx-ext"] = value; }
	}

	public string? hxBoost
	{
		set { dict["hx-boost"] = value; }
	}

	public string? hxPushUrl
	{
		set { dict["hx-push-url"] = value; }
	}

	public string? hxHeaders
	{
		set { dict["hx-headers"] = value; }
	}

	public string? hxParams
	{
		set { dict["hx-params"] = value; }
	}

	public string? hxHistory
	{
		set { dict["hx-history"] = value; }
	}

	public string? hxTrigger
	{
		set { dict["hx-trigger"] = value; }
	}

	public string? jsonVals
	{
		set
		{
			dict["hx-ext"] = "hx-noformdata";
			dict["jsonVals"] = value;
		}
	}

}

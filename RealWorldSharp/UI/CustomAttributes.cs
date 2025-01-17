namespace RealWorldSharp.UI;

public class CustomAttributes : HtmlAttributes
{

	public string hxBindVals
	{
		set
		{
			hxExt = "hx-noformdata";
			xBind = ("hx-vals", value);
		}
	}

	public string? hxTargetInner
	{
		set
		{
			hxTarget = value;
			hxSwap = "innerHTML";
		}
	}

	public string? hxGetRef
	{
		set
		{
			hxGet = value;
			href = value;
			hxPushUrl = "true";
		}
	}

	public string? hxPostRef
	{
		set
		{
			hxPost = value;
			href = value;
			hxPushUrl = "true";
		}
	}

}

namespace RealWorldSharp.UI;

public static class CustomHtml
{
	public static HtmlElement button(CustomAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("button", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement a(CustomAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("a", attrs, elements);
		return elem;
	}

}

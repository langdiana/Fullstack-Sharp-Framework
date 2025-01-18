namespace RealWorldSharp.UI;

public static class CustomHtml
{
	public static InlineHtmlElement button(CustomAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("button", attrs, elements);
		return elem;
	}

	// HTML5: Anchor element can contain block elements
	public static InlineHtmlElement a(CustomAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("a", attrs, elements);
		return elem;
	}

}

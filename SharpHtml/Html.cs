namespace SharpHtml;

public static partial class Html
{
	public static HtmlAttributes? _ { get; set; } = null;

	// custom
	public static HtmlElement Frag(params HtmlElement[] elements)
	{
		var elem = new HtmlElement(null, null, elements);
		return elem;
	}


	// inline?
	public static HtmlElement dialog(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("dialog", attrs, elements);
		return elem;
	}


}

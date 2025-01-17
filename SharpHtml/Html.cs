namespace SharpHtml;

/*
public class NameValuePair
{
	public required string Name { get; set; }
	public string? Value { get; set; }

	//public static implicit operator NameValuePair(in (string key, string value) tuple) => new NameValuePair() { Name = tuple.key, Value = tuple.value };

}
*/

public static partial class Html
{
	public static HtmlAttributes? _ { get; set; } = null;

	// syntetic
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

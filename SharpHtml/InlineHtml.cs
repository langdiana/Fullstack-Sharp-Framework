using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpHtml;

public static partial class Html
{
	public static InlineHtmlElement button(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("button", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement span(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("span", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement input(HtmlAttributes? attrs = null)
	{
		var elem = new InlineHtmlElement("input", attrs);
		return elem;
	}

	public static InlineHtmlElement hr(HtmlAttributes? attrs = null)
	{
		var elem = new InlineHtmlElement("hr", attrs);
		return elem;
	}

	public static InlineHtmlElement br(HtmlAttributes? attrs = null)
	{
		var elem = new InlineHtmlElement("br", attrs);
		return elem;
	}

	public static InlineHtmlElement label(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("label", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement textarea(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("textarea ", attrs, elements);
		return elem;
	}

	// HTML5: Anchor element can contain block elements
	public static InlineHtmlElement a(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("a", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement i(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("i", attrs, elements);
		return elem;
	}

	public static InlineHtmlElement img(HtmlAttributes? attrs = null, params InlineHtmlElement[] elements)
	{
		var elem = new InlineHtmlElement("img", attrs, elements);
		return elem;
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpHtml;

public static partial class Html
{
	public static HtmlBody body(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlBody();
		return elem;
	}

	public static HtmlElement div(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("div", attrs, elements);
		return elem;
	}

	public static HtmlElement ul(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("ul", attrs, elements);
		return elem;
	}

	public static HtmlElement ol(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("ol", attrs, elements);
		return elem;
	}

	public static HtmlElement template(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("template", attrs, elements);
		return elem;
	}

	public static HtmlElement li(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("li", attrs, elements);
		return elem;
	}

	public static HtmlElement form(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("form", attrs, elements);
		return elem;
	}

	public static HtmlElement fieldset(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("fieldset", attrs, elements);
		return elem;
	}

	public static HtmlElement p(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("p", attrs, elements);
		return elem;
	}

	public static HtmlElement embed(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("embed", attrs, elements);
		return elem;
	}

	public static HtmlElement header(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("header", attrs, elements);
		return elem;
	}

	public static HtmlElement footer(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("footer", attrs, elements);
		return elem;
	}

	public static HtmlElement h1(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("h1", attrs, elements);
		return elem;
	}

	public static HtmlElement h2(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("h2", attrs, elements);
		return elem;
	}

	public static HtmlElement h3(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("h3", attrs, elements);
		return elem;
	}

	public static HtmlElement h4(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("h4", attrs, elements);
		return elem;
	}

	public static HtmlElement main(params HtmlElement[] elements)
	{
		var elem = new HtmlElement("main", null, elements);
		return elem;
	}

	public static HtmlElement nav(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("nav", attrs, elements);
		return elem;
	}

	public static HtmlElement table(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("table", attrs, elements);
		return elem;
	}

	public static HtmlElement tr(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("tr", attrs, elements);
		return elem;
	}

	public static HtmlElement tbody(HtmlAttributes? attrs = null, params HtmlElement[] elements)
	{
		var elem = new HtmlElement("tbody", attrs, elements);
		return elem;
	}

	public static HtmlElement script(string text)
	{
		var elem = new HtmlElement("script", null, text);
		return elem;
	}

}

namespace SharpHtml;

public class HtmlElement
{
	public HtmlElement(string? tag, HtmlAttributes? attrs, params HtmlElement[] elements)
	{
		this.tag = tag;

		if (attrs != null)
			Attributes = attrs;

		if (elements != null)
			foreach (var element in elements)
				Elements.Add(element);
	}

	bool isSelfClosing;

	public HtmlElement(string tag, HtmlAttributes? attrs = null) // self closing
	{
		this.tag = tag;

		if (attrs != null)
			Attributes = attrs;

		isSelfClosing = true;
	}

	protected string? tag;

	public List<HtmlElement> Elements { get; set; } = new();
	public HtmlAttributes Attributes { get; set; } = new();
	public string? Text { get; set; }
	public bool Inline { get; protected set; }

	public virtual string Render()
	{
		if (tag == null)
		{
			return @$"{RenderChildren()}";
		}
		else
		{
			if (isSelfClosing)
				return @$"<{tag} {Attributes.Text}>";

			else
				return @$"<{tag} {Attributes.Text}>{Text} {RenderChildren()}</{tag}>
";
		}

	}

	protected virtual string RenderChildren()
	{
		string html = "";
		foreach (var element in Elements)
		{
			html += element.Render();
		}

		return html;
	}

	public void Add(HtmlElement element)
	{
		Elements.Add(element);
	}

	public static implicit operator HtmlElement(in string value) => new InlineText(value);

}

public class InlineHtmlElement: HtmlElement
{

	public InlineHtmlElement(string? tag, HtmlAttributes? attrs, params InlineHtmlElement[] elements): base(tag,attrs,elements)
	{
		Inline = true;
	}

	public InlineHtmlElement(string? tag, HtmlAttributes? attrs, params HtmlElement[] elements) : base(tag, attrs, elements)
	{
		Inline = true;
	}

	public static implicit operator InlineHtmlElement(in string value) => new InlineText(value);
}

public class InlineText : InlineHtmlElement
{

	public InlineText(string text) : base(null, null)
	{
		Text = text;
	}

	public override string Render()
	{
		return Text == null ? "" : Text;
	}

}


public class HtmlBody : HtmlElement
{
	public HtmlBody() : base("")
	{
	}

	public string? Head { get; set; }

	public override string Render()
	{
		return @$"
<!DOCTYPE html>
<html>
<head>
{Head}
</head>
<body>
    {RenderChildren()}
</body>
</html>
";
	}

}

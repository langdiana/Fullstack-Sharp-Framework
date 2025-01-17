namespace RealWorldSharp.UI;


public static partial class Layout
{
	public static HtmlElement Footer()
	{
		return

		footer(_,
			div(new() { className = "container" },
				a(new() { href = "/", className = "logo-font" }, "conduit"),
				span(new() { className = "attribution" }, "An interactive learning project from", a(new() { href = "https://thinkster.io" }, "Thinkster"), @". Code &amp;
      design licensed under MIT.")
			)
		);
	}

}


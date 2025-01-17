namespace RealWorldSharp.UI;

public class AppBody
{

	public HtmlElement Render(HtmlElement mainContent, HtmlElement header)
	{
		return
		div(new() { hxBoost = "true", hxTarget = Targets.MainId.Target },
			header,
			div(new() { id = Targets.MainId.Id },
				mainContent
			),
			Footer()
		);
	}

}

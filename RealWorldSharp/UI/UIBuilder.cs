namespace RealWorldSharp.UI;

public class UIBuilder: IUIBuilder
{
	public UIBuilder(bool isHtmx, string? username)
	{
		this.isHtmx = isHtmx;
		this.username = username;
	}

	bool isHtmx;
	string? username;

	protected virtual IResult RenderApp(HtmlElement mainContent)
	{
		var appHead = new AppHead();
		var app = appHead.Build();
		var appBody = new AppBody();
		var header = username != null ? HeaderAuth(username) : HeaderUnauth();
		app.Add(appBody.Render(mainContent, header));
		var html = app.Render();
		return Results.Content(html, contentType: "text/html");
	}

	public virtual IResult RenderPage(HtmlElement page)
	{
		if (!isHtmx)
			return RenderApp(page);

		var html = page.Render();
		return Results.Content(html, contentType: "text/html");
	}

	public virtual IResult RenderPages(HtmlElement page, List<HtmlElement> oobPages)
	{
		var html = page.Render();
		foreach (var oobPage in oobPages)
		{
			html += "\r\n";
			html += oobPage.Render();
		}

		return Results.Content(html, contentType: "text/html");
	}


}

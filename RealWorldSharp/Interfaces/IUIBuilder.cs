namespace RealWorldSharp.Interfaces;

public interface IUIBuilder
{
	IResult RenderPage(HtmlElement page);
	IResult RenderPages(HtmlElement page, List<HtmlElement> oobPages);
}

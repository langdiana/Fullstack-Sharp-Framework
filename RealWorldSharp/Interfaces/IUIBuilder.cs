using SharpHtml;

namespace RealWorldSharp.Interfaces;

public interface IUIBuilder
{
	//IResult RenderApp(HtmlElement appBody);
	IResult RenderPage(HtmlElement page);
	IResult RenderPages(HtmlElement page, List<HtmlElement> oobPages);
}

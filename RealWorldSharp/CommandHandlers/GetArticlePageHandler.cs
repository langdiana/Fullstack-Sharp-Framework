namespace RealWorldSharp.CommandHandlers;

public class GetArticlePageHandler : CommandHandlerBase<GetArticlePageCommand>
{

	public override async Task Execute(GetArticlePageCommand cmd)
	{
		var page = await GetArticlePage(cmd.Slug);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

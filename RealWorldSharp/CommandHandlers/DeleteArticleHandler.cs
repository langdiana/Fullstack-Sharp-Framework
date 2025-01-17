namespace RealWorldSharp.CommandHandlers;

public class DeleteArticleHandler : CommandHandlerBase<DeleteArticleCommand>
{

	public override async Task Execute(DeleteArticleCommand cmd)
	{
		await Repo.DeleteArticle(cmd.ArticleId);

		var home = await GetHomePage(FeedTypeEnum.Global);
		cmd.Result = UiBuilder.RenderPage(home);
	}
}

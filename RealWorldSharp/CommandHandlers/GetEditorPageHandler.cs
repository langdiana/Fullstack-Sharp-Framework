namespace RealWorldSharp.CommandHandlers;

public class GetEditorPageHandler : CommandHandlerBase<GetEditorPageCommand>
{

	public override async Task Execute(GetEditorPageCommand cmd)
	{
		ArticleModel? article;
		if (cmd.Slug == null)
		{
			article = new ArticleModel();
		}
		else
		{
			article = await Repo.GetArticleBySlug(cmd.Slug);
			if (article == null)
			{
				cmd.Result = UiBuilder.RenderPage(ErrorPage("Article not found", null));
				return;
			}
		}

		var page = EditorPage(article, null);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}


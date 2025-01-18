namespace RealWorldSharp.CommandHandlers;

public class PostArticleHandler : CommandHandlerBase<PostArticleCommand>
{

	public override async Task Execute(PostArticleCommand cmd)
	{
		var article = cmd.Article.ToEntity();
		string? errorMessage = null;
		if (article.Title == null)
			errorMessage = "Title cannot be empty";
		else if (article.Description == null)
			errorMessage = "Description cannot be empty";
		else if (article.Body == null)
			errorMessage = "Body cannot be empty";

		if (errorMessage != null)
		{
			var newpage = EditorPage(cmd.Article, errorMessage);
			cmd.Result = UiBuilder.RenderPage(newpage);
			cmd.ErrorMessage = errorMessage;
			return;
		}

		article.UserId = cmd.UserId.GetValueOrDefault();
		article.Slug = Guid.NewGuid().ToString();
		article.CreatedAt = DateTime.UtcNow;
		await Repo.Save(article);

		var page = await GetArticlePage(article.ArticleId);
		cmd.Result = UiBuilder.RenderPage(page);
	}

}

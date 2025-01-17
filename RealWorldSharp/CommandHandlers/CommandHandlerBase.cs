using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.CommandHandlers;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : CommandBase
{
	public IRealWorldRepo Repo { get; init; } = null!;
	public IUIBuilder UiBuilder { get; init; } = null!;
	public IAuthService AuthService { get; init; } = null!;


	User? crtUser;
	protected async Task<User?> GetCrtUser(int? userId)
	{
		if (userId == null)
			return null;
		else if (crtUser != null)
			return crtUser;
		else
		{
			crtUser = await Repo.GetUserById(userId.Value);
			return crtUser;
		}
	}
	
	public abstract Task Execute(TCommand cmd);

	protected async Task<HtmlElement> GetHomePage(FeedTypeEnum feedType, string? tag = null)
	{
		var homeModel = await Repo.GetHomeModel(feedType, tag);
		return HomePage(homeModel, feedType);
	}

	HtmlElement GetArticlePage(ArticleModel? article)
	{
		if (article == null)
			return ErrorPage("Article not found", null);

		return ArticlePage(article);
	}

	protected async Task<HtmlElement> GetArticlePage(int articleId)
	{
		var article = await Repo.GetArticleById(articleId);
		return GetArticlePage(article);
	}

	protected async Task<HtmlElement> GetArticlePage(string slug)
	{
		var article = await Repo.GetArticleBySlug(slug);
		return GetArticlePage(article);
	}

}

using RealWorldSharp.Repos;

namespace RealWorldSharp.Services;

public class RealWorldService: ServiceBase
{
	public RealWorldService(RealWorldContext dbContext, IHttpContextAccessor contextAccessor, ILogger<CommandBase> logger) : base(logger)
	{
		if (contextAccessor.HttpContext != null)
			httpContext = contextAccessor.HttpContext;
		else
			throw new Exception("Invalid configuration");

		var headers = httpContext.Request.Headers;
		if (headers != null)
		{
			isHtmx = headers.ContainsKey("HX-Request");
			//var htmx2 = headers["HX-History-Restore-Request"] == "true";
			//isHtmx = isHtmx && !htmx2;
		}

		Repo = new RealWorldRepo(dbContext, httpContext.RequestAborted);
		AuthService = new AuthService(httpContext);
	}

	IRealWorldRepo Repo;
	IAuthService AuthService;
	IUIBuilder UiBuilder = null!;

	HttpContext httpContext;
	bool isHtmx = false;
	string? userEmail =  null;
	string? username = null;
	int? userId = null;

	async Task InitUser()
	{
		ClaimsPrincipal? user = httpContext.User;

		var userIdClaim = user.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
		if (userIdClaim != null)
			userId = Int32.Parse(userIdClaim.Value);

		var crtUser = await Repo.SetCrtUser(userId);
		if (crtUser == null)
		{
			userId = null;
		}
		else
		{
			userEmail = crtUser.Email;
			username = crtUser.Username;
		}
		
		UiBuilder = new UIBuilder(isHtmx, username);
	}

	void InitCommand(CommandBase cmd)
	{
		cmd.CancellationToken = httpContext.RequestAborted;
		cmd.IsHtmx = isHtmx;
		cmd.UserEmail = userEmail;
		cmd.UserId = userId;
	}

	async Task<IResult> Execute<TCommand, TCommandHandler>(TCommand cmd)
		where TCommand : CommandBase
		where TCommandHandler: CommandHandlerBase<TCommand>, new()		
	{

		await InitUser();
		InitCommand(cmd);

		TCommandHandler handler = new() { Repo = Repo, UiBuilder = UiBuilder, AuthService = AuthService };
		await Handle(handler, cmd);
		if (cmd.IsCriticalError)
			return UiBuilder.RenderPage(ErrorPage(cmd.ErrorMessage ?? "", cmd.ErrorDetail));
		else
			return cmd.Result;
	}

	public async Task<IResult> GetHomePage()
	{
		var cmd = new GetHomePageCommand { };
		return await Execute<GetHomePageCommand, GetHomePageHandler>(cmd);
	}

	public async Task<IResult> PostLogin(LoginModel login)
	{
		var cmd = new LoginCommand { Login = login};
		return await Execute<LoginCommand, LoginHandler>(cmd);
	}

	public async Task<IResult> GetLoginPage()
	{
		var cmd = new GetLoginPageCommand();
		return await Execute<GetLoginPageCommand, GetLoginPageHandler>(cmd);
	}

	public async Task<IResult> Logout()
	{
		var cmd = new LogoutCommand { };
		return await Execute<LogoutCommand, LogoutHandler>(cmd);
	}

	public async Task<IResult> GetRegisterPage()
	{
		var cmd = new GetRegisterPageCommand();
		return await Execute<GetRegisterPageCommand, GetRegisterPageHandler>(cmd);
	}

	public async Task<IResult> PostRegister(RegisterModel register)
	{
		var cmd = new PostRegisterCommand { Register = register };
		return await Execute<PostRegisterCommand, PostRegisterHandler>(cmd);
	}

	public async Task<IResult> GetProfilePage(string username, bool isMyArticles)
	{
		var cmd = new GetProfilePageCommand { Username = username, IsMyArticles = isMyArticles };
		return await Execute<GetProfilePageCommand, GetProfilePageHandler>(cmd);
	}

	public async Task<IResult> GetSettingsPage()
	{
		var cmd = new GetSettingsPageCommand { };
		return await Execute<GetSettingsPageCommand, GetSettingsPageHandler>(cmd);
	}

	public async Task<IResult> PostSettings(UserModel user)
	{
		var cmd = new PostSettingsCommand { User = user };
		return await Execute<PostSettingsCommand, PostSettingsHandler>(cmd);
	}

	public async Task<IResult> GetEditorPage(string? slug)
	{
		var cmd = new GetEditorPageCommand { Slug = slug };
		return await Execute<GetEditorPageCommand, GetEditorPageHandler>(cmd);
	}

	public async Task<IResult> GetArticlePage(string slug)
	{
		var cmd = new GetArticlePageCommand { Slug = slug};
		return await Execute<GetArticlePageCommand, GetArticlePageHandler>(cmd);
	}

	public async Task<IResult> DeleteArticle(int articleId)
	{
		var cmd = new DeleteArticleCommand { ArticleId = articleId };
		return await Execute<DeleteArticleCommand, DeleteArticleHandler>(cmd);
	}

	public async Task<IResult> PostArticle(ArticleModel article)
	{
		var cmd = new PostArticleCommand { Article = article };
		return await Execute<PostArticleCommand, PostArticleHandler>(cmd);
	}

	public async Task<IResult> GetFeed(FeedTypeEnum feedType, string? tag = null)
	{
		var cmd = new GetFeedCommand { FeedType = feedType, Tag = tag };
		return await Execute<GetFeedCommand, GetFeedHandler>(cmd);
	}

	public async Task<IResult> ToggleFavorite(ArticleFavoriteModel favorite, bool isView)
	{
		var cmd = new FavoriteCommand { Favorite = favorite, IsView = isView };
		return await Execute<FavoriteCommand, FavoriteHandler>(cmd);
	}

	public async Task<IResult> ToggleFollowUser(FollowUserModel follow)
	{
		var cmd = new FollowCommand { Follow = follow };
		return await Execute<FollowCommand, FollowHandler>(cmd);
	}

	public async Task<IResult> ToggleFollowUserProfile(FollowUserProfileModel follow)
	{
		var cmd = new FollowProfileCommand { Follow = follow };
		return await Execute<FollowProfileCommand, FollowProfileHandler>(cmd);
	}

	public async Task<IResult> PostComment(PostCommentModel comment)
	{
		var cmd = new PostCommentCommand { Comment = comment };
		return await Execute<PostCommentCommand, PostCommentHandler>(cmd);
	}

	public async Task<IResult> DeleteComment(DeleteCommentModel comment)
	{
		var cmd = new DeleteCommentCommand { Comment = comment };
		return await Execute<DeleteCommentCommand, DeleteCommentHandler>(cmd);
	}

	public async Task<IResult> GetHomePager(HomePagerModel pager)
	{
		var cmd = new GetHomePagerCommand { Pager = pager };
		return await Execute<GetHomePagerCommand, GetHomePagerHandler>(cmd);
	}

	public async Task<IResult> GetProfilePager(ProfilePagerModel pager)
	{
		var cmd = new GetProfilePagerCommand { Pager = pager };
		return await Execute<GetProfilePagerCommand, GetProfilePagerHandler>(cmd);
	}

}

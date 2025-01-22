namespace RealWorldSharp.Commands;

public class GetHomePageCommand: CommandBase
{
}

public class GetLoginPageCommand: CommandBase
{
}

public class LoginCommand: CommandBase
{
	public LoginModel Login = null!;
}

public class LogoutCommand : CommandBase
{
}

public class GetRegisterPageCommand : CommandBase
{
}

public class PostRegisterCommand : CommandBase
{
	public RegisterModel Register = null!;
}

public class GetProfilePageCommand : CommandBase
{
	public string Username = null!;
	public bool IsMyArticles;
}

public class GetSettingsPageCommand : CommandBase
{
}

public class PostSettingsCommand : CommandBase
{
	public UserModel User = null!;
}

public class GetEditorPageCommand : CommandBase
{
	public string? Slug;
}

public class GetArticlePageCommand : CommandBase
{
	public string Slug = null!;
}

public class DeleteArticleCommand : CommandBase
{
	public int ArticleId;
}

public class PostArticleCommand : CommandBase
{
	public ArticleModel Article = null!;
}

public class GetFeedCommand : CommandBase
{
	public FeedTypeEnum FeedType; 
	public string? Tag;
}

public class FavoriteCommand : CommandBase
{
	public ArticleFavoriteModel Favorite = null!;
	public bool IsView;
}

public class FollowCommand : CommandBase
{
	public FollowUserModel Follow = null!;
}

public class FollowProfileCommand : CommandBase
{
	public FollowUserProfileModel Follow = null!;
}

public class PostCommentCommand : CommandBase
{
	public PostCommentModel Comment = null!;
}

public class DeleteCommentCommand : CommandBase
{
	public DeleteCommentModel Comment = null!;
}

public class GetHomePagerCommand : CommandBase
{
	public HomePagerModel Pager = null!;
}

public class GetProfilePagerCommand : CommandBase
{
	public ProfilePagerModel Pager = null!;
}

#region repo

public class GetArticlesForProfileCommand : CommandBase
{
	public ProfilePagerInfo PagerInfo = new();

	[JsonIgnore]
	public List<ArticleModel> Articles = null!;
}

public class GetArticlesForHomeCommand : CommandBase
{
	public HomePagerInfo PagerInfo = new();

	[JsonIgnore]
	public List<ArticleModel> Articles = null!;
}

#endregion repo

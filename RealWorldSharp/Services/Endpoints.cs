namespace RealWorldSharp.Services;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet($"{Routes.Article}{{slug}}", (RealWorldService service, string slug) => service.GetArticlePage(slug));
        routes.MapPost(Routes.ArticleFav, (RealWorldService service, ArticleFavoriteModel favModel) => service.ToggleFavorite(favModel, false));
        routes.MapPost(Routes.ArticleViewFav, (RealWorldService service, ArticleFavoriteModel favModel) => service.ToggleFavorite(favModel, true));
        routes.MapPost(Routes.FollowUser, (RealWorldService service, FollowUserModel follow) => service.ToggleFollowUser(follow));
        routes.MapPost(Routes.PostComment, (RealWorldService service, PostCommentModel comment) => service.PostComment(comment));
        routes.MapPost(Routes.DeleteComment, (RealWorldService service, DeleteCommentModel comment) => service.DeleteComment(comment));
        routes.MapDelete($"{Routes.ArticleDelete}{{articleId}}", (RealWorldService service, int articleId) => service.DeleteArticle(articleId));
		routes.MapPost(Routes.HomePager, (RealWorldService service, HomePagerModel pager) => service.GetHomePager(pager));
		routes.MapPost(Routes.ProfilePager, (RealWorldService service, ProfilePagerModel pager) => service.GetProfilePager(pager));

		routes.MapGet(Routes.Editor, (RealWorldService service) => service.GetEditorPage(null));
        routes.MapGet($"{Routes.Editor}{{slug}}", (RealWorldService service, string slug) => service.GetEditorPage(slug));
        routes.MapPost(Routes.Editor, (RealWorldService service, ArticleModel article) => service.PostArticle(article));

        routes.MapGet(Routes.Home, (RealWorldService service) => service.GetHomePage());
        routes.MapGet(Routes.YourFeed, (RealWorldService service) => service.GetFeed(FeedTypeEnum.Yours));
        routes.MapGet(Routes.GlobalFeed, (RealWorldService service) => service.GetFeed(FeedTypeEnum.Global));
        routes.MapGet($"{Routes.TagFeed}{{tag}}", (RealWorldService service, string tag) => service.GetFeed(FeedTypeEnum.Tag, tag));

        routes.MapGet(Routes.Login, (RealWorldService service) => service.GetLoginPage());
        routes.MapPost(Routes.Login, (RealWorldService service, LoginModel login) => service.PostLogin(login));
        routes.MapGet(Routes.Logout, (RealWorldService service) => service.Logout());

        routes.MapGet($"{Routes.Profile}{{username}}", (RealWorldService service, string username) => service.GetProfilePage(username, true));
        routes.MapGet($"{Routes.Profile}{{username}}/myarticles", (RealWorldService service, string username) => service.GetProfilePage(username, true));
        routes.MapGet($"{Routes.Profile}{{username}}/favorited", (RealWorldService service, string username) => service.GetProfilePage(username, false));
		routes.MapPost(Routes.ProfileFollowUser, (RealWorldService service, FollowUserProfileModel follow) => service.ToggleFollowUserProfile(follow));

		routes.MapGet(Routes.Register, (RealWorldService service) => service.GetRegisterPage());
        routes.MapPost(Routes.Register, (RealWorldService service, RegisterModel register) => service.PostRegister(register));

        routes.MapGet(Routes.Settings, (RealWorldService service) => service.GetSettingsPage());
		routes.MapPost(Routes.Settings, (RealWorldService service, UserModel user) => service.PostSettings(user));

	}
}


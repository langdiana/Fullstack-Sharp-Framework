namespace RealWorldSharp.UI;

public static class Routes
{
	public const string Home = "/";
	public const string Login = "/login";
	public const string Logout = "/logout";
	public const string Register = "/register";
	public const string Settings = "/settings";
	public const string Editor = "/editor/";
	public const string Profile = "/profile/";
	public const string ProfileFollowUser = "/profile/follow";
	public const string Article = "/article/";
	public const string ArticleFav = "/article/fav";
	public const string ArticleDelete = "/article/delete/";
	public const string ArticleViewFav = "/article/view/fav";
	public const string HomePager = "/article/pager";
	public const string ProfilePager = "/profile/pager";
	public const string FollowUser = "/article/follow";
	public const string PostComment = "/article/comment";
	public const string DeleteComment = "/article/comment/delete";
	public const string GlobalFeed = "/globalfeed";
	public const string YourFeed = "/yourfeed";
	public const string TagFeed = "/tagfeed";
}

public class IdTarget
{
	public IdTarget(string id)
	{
		Id = id;
	}

	public string Id { get; }
	public string Target => $"#{Id}";

}

static class Targets
{
	public static IdTarget MainId = new("appbody");
	public static IdTarget Header = new("header");
	public static IdTarget Feed = new ("feed");
	public static IdTarget Article = new("articlepage");
	public static IdTarget FollowCounter1 = new("followcounter1");
	public static IdTarget FollowCounter2 = new("followcounter2");
	public static IdTarget FollowCounterProfile = new("followcounterprofile");
	public static IdTarget FavCounter = new("favcounter");
	public static IdTarget FavCounterView1 = new("favcounterview1");
	public static IdTarget FavCounterView2 = new("favcounterview2");
	public static IdTarget PageCounter = new("pagecounter");
}

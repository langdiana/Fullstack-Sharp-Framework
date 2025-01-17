namespace RealWorldSharp.UI.Components;

public static partial class Compo
{
	public static HtmlElement FollowCounter(ArticleModel article, string id, bool isOob, bool isAuthenticated)
	{
		var following = article.Author.Following;
		string userName = article.Author.Username;
		int counter = article.Author.FollowerUsers.Count;
		string className = following ? "ion-minus-round" : "ion-plus-round";
		string follow = following ? "Unfollow" : "Follow";

		HtmlAttributes attr;
		if (isAuthenticated)
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-secondary",
				id = id,
				hxPost = Routes.FollowUser,
				hxTarget = Targets.FollowCounter1.Target,
				hxSwap = "outerHTML",
				jsonVals = article.GetFollowJson(),
				hxOob = isOob
			};
		}
		else
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-secondary",
				hxGet = Routes.Login,
				hxSwap = "innerHTML",
				hxTarget = Targets.MainId.Target,
			};
		}

		return
		button(attr,
			i(new() { className = className }), $"&nbsp; {follow} {userName}",
				span(new() { className = "counter" }, $"({counter})")
		);
	}

}

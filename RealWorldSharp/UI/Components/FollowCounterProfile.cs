using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.UI.Components;

public static partial class Compo
{
	public static HtmlElement FollowCounterProfile(ProfileModel profile, string id, bool isAuthenticated)
	{
		var following = profile.Following;
		string userName = profile.Username;
		int counter = profile.FollowerCount;
		string className = following ? "ion-minus-round" : "ion-plus-round";
		string follow = following ? "Unfollow" : "Follow";

		HtmlAttributes attr;
		if (isAuthenticated)
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-secondary action-btn",
				hxPost = Routes.ProfileFollowUser,
				hxTarget = Targets.FollowCounterProfile.Target,
				hxSwap = "outerHTML",
				jsonVals = profile.FollowJson,
				id = id,
			};
		}
		else
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-secondary action-btn",
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


namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement ProfilePage(ProfileModel profile, bool isMyArticles)
	{
		var myLink = $"{Routes.Profile}{profile.Username}/myarticles";
		var favLink = $"{Routes.Profile}{profile.Username}/favorited";
		string activeMy = isMyArticles ? "active" : "";
		string activeFav = isMyArticles ? "" : "active";

		return
		div(new() { className = "profile-page",  },
			div(new() { className = "user-info" },
				div(new() { className = "container" },
					div(new() { className = "row" },
						div(new() { className = "col-xs-12 col-md-10 offset-md-1" },
							img(new() { src = profile.Image, className = "user-img" }),
							h4(_, profile.Username
							),
							p(_, profile.Bio ?? ""
							),
							profile.IsCrtUser ? Frag() : FollowCounterProfile(profile, Targets.FollowCounterProfile.Id, profile.CrtUser != null),
							!profile.IsCrtUser ? Frag() : button(new() { className = "btn btn-sm btn-outline-secondary action-btn", hxGet = Routes.Settings },
								i(new() { className = "ion-gear-a" }), "&nbsp; Edit Profile Settings"
							)
						)
					)
				)
			),
			div(new() { className = "container" },
				div(new() { className = "row" },
					div(new() { className = "col-xs-12 col-md-10 offset-md-1" },
						div(new() { className = "articles-toggle" },
							ul(new() { className = "nav nav-pills outline-active" },
								li(new() { className = "nav-item" },
									a(new() { className = $"nav-link {activeMy}", href = myLink }, "My Articles")
								),
								li(new() { className = "nav-item" },
									a(new() { className = $"nav-link {activeFav}", href = favLink }, "Favorited Articles")
								)
							)
						),

						ArticleList(profile.Articles, profile.PagerInfo, profile.CrtUser != null)
					)
				)
			)
		);
	}

}
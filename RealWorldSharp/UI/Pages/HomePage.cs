namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement HomePage(HomeModel homeModel, FeedTypeEnum feedType)
	{
		bool isAuthenticated = homeModel.CrtUser != null;
		string activeGlobal = feedType is FeedTypeEnum.Global ? "active" : "";
		string activeYour = feedType is FeedTypeEnum.Yours ? "active" : "";

		return
		div(new() { className = "home-page", }, // hxHistory = "false" },
			div(new() { className = "banner" },
				div(new() { className = "container" },
					h1(new() { className = "logo-font" }, "conduit"
					),
					p(_, "A place to share your knowledge."
					)
				)
			),
			div(new() { className = "container page" },
				div(new() { className = "row" },
					div(new() { className = "col-md-9" },
						div(new() { className = "feed-toggle" },
							ul(new() { className = "nav nav-pills outline-active" },
								isAuthenticated ? li(new() { className = "nav-item" },
									a(new() { className = $"nav-link {activeYour}", href = Routes.YourFeed }, "Your Feed")
								) : Frag(),
								li(new() { className = "nav-item" },
									a(new() { className = $"nav-link {activeGlobal}", href = Routes.GlobalFeed }, "Global Feed")
								)
							)
						),

						ArticleList(homeModel.Articles, homeModel.PagerInfo, homeModel.CrtUser != null)
					),
					div(new() { className = "col-md-3" },
						div(new() { className = "sidebar" },
							p(_, "Popular Tags"
							),
							div(new() { className = "tag-list" },
								TagList(homeModel.Tags)
							)
						)
					)
				)
			)
		);
	}

	public static HtmlElement TagList(List<Tag> tags)
	{
		var root = Frag();

		foreach (var tag in tags)
		{
			var tagLink = $"{Routes.TagFeed}{tag.TagName}";

			var itemElem =
			a(new() { className = "tag-pill tag-default", href = tagLink}, tag.TagName);

			root.Add(itemElem);
		}

		return root;
	}

}

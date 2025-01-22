namespace RealWorldSharp.UI.Components;

public static partial class Compo
{
	public static HtmlElement FavoriteCounterView(string id, ArticleModel article, bool isOob, bool isAuthenticated)
	{
		HtmlAttributes attr;
		if (isAuthenticated)
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-primary",
				id = id,
				hxPost = Routes.ArticleViewFav,
				hxTarget = Targets.FavCounterView1.Target,
				hxSwap = "outerHTML",
				jsonVals = article.GetFavoriteJson(),
				hxOob = isOob
			};
		}
		else
		{
			attr = new()
			{
				className = "btn btn-sm btn-outline-primary",
				hxGet = Routes.Login,
				hxSwap = "innerHTML",
				hxTarget = Targets.MainId.Target,
			};
		}

		return
		button(attr,
			i(new() { className = "ion-heart" }), "&nbsp; Favorite Post",
			span(new() { className = "counter" }, $"({article.FavoritesCount})")
		);
		
	}

}


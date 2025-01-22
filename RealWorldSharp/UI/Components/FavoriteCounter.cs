namespace RealWorldSharp.UI.Components;

public static partial class Compo
{
	public static HtmlElement FavoriteCounter(ArticleModel article, bool isAuthenticated)
	{
		HtmlAttributes attr;
		if (isAuthenticated)
		{
			attr = new()
			{
				className = "btn btn-outline-primary btn-sm pull-xs-right",
				id = Targets.FavCounter.Id + article.ArticleId.ToString(),
				disabled = article.IsAuthor,
				hxPost = Routes.ArticleFav,
				hxSwap = "outerHTML",
				hxTarget = Targets.FavCounter.Target + article.ArticleId.ToString(),
				jsonVals = article.GetFavoriteJson()
			};
		}
		else
		{
			attr = new()
			{
				className = "btn btn-outline-primary btn-sm pull-xs-right",
				hxGet = Routes.Login,
				hxSwap = "innerHTML",
				hxTarget = Targets.MainId.Target,
			};
		}

		return
		button(attr,
			i(new() { className = "ion-heart" }, $"{article.FavoritesCount}")
		);

	}

}


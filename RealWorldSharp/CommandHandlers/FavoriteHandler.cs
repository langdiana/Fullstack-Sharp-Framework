using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.CommandHandlers;

public class FavoriteHandler : CommandHandlerBase<FavoriteCommand>
{

	public override async Task Execute(FavoriteCommand cmd)
	{
		ArticleFavorite? fav;

		fav = await Repo.FirstOrDefault<ArticleFavorite>(x => x.ArticleId == cmd.Favorite.ArticleId && x.UserId == cmd.UserId);

		if (fav != null)
		{
			Repo.Remove(fav);
			await Repo.SaveChanges();
		}
		else
		{
			fav = new()
			{
				ArticleId = cmd.Favorite.ArticleId,
				UserId = cmd.UserId.GetValueOrDefault(),
			};

			await Repo.Save(fav);
		}
		var article = await Repo.GetArticleById(cmd.Favorite.ArticleId);
		if (article == null)
		{
			cmd.Result = UiBuilder.RenderPage(ErrorPage("Article not found", null));
			return;
		}

		if (cmd.IsView)
		{
			var page1 =
			FavoriteCounterView(Targets.FavCounterView1.Id, article, false, true);
			var page2 =
			FavoriteCounterView(Targets.FavCounterView2.Id, article, true, true);

			cmd.Result = UiBuilder.RenderPages(page1, [page2]);
		}
		else
		{
			var page =
			FavoriteCounter(article, true);
			cmd.Result = UiBuilder.RenderPage(page);
		}
	}
}

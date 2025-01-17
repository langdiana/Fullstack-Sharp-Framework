namespace RealWorldSharp.CommandHandlers;

public class GetProfilePagerHandler : CommandHandlerBase<GetProfilePagerCommand>
{

	public override async Task Execute(GetProfilePagerCommand cmd)
	{
		GetArticlesForProfileCommand cmdRepo = new();
		cmdRepo.PagerInfo.IsMyArticles = cmd.Pager.IsMyArticles;
		cmdRepo.PagerInfo.ProfileUserId = cmd.Pager.ProfileUserId;
		cmdRepo.PagerInfo.PageNumber = cmd.Pager.PageNumber;

		await Repo.GetArticlesForProfile(cmdRepo);
		var page =
		ArticleList(cmdRepo.Articles, cmdRepo.PagerInfo, true);

		cmd.Result = UiBuilder.RenderPage(page);
	}
}

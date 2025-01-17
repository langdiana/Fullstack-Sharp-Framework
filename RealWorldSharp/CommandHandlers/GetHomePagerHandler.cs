namespace RealWorldSharp.CommandHandlers;

public class GetHomePagerHandler : CommandHandlerBase<GetHomePagerCommand>
{

	public override async Task Execute(GetHomePagerCommand cmd)
	{
		GetArticlesForHomeCommand cmdRepo = new();
		cmdRepo.PagerInfo.FeedType = cmd.Pager.FeedType;
		cmdRepo.PagerInfo.Tag = cmd.Pager.Tag;
		cmdRepo.PagerInfo.PageNumber = cmd.Pager.PageNumber;

		await Repo.GetArticlesForHome(cmdRepo);
		var page =
		ArticleList(cmdRepo.Articles, cmdRepo.PagerInfo, true);

		cmd.Result = UiBuilder.RenderPage(page);
	}
}

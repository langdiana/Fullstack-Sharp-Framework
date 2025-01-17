
namespace RealWorldSharp.CommandHandlers;

public class FollowHandler : CommandHandlerBase<FollowCommand>
{
	public override async Task Execute(FollowCommand cmd)
	{
		if (cmd.UserId == null)
			return;

		UserFollow? userFollow = await Repo.FirstOrDefault<UserFollow>(x => x.SourceUserId == cmd.UserId && x.TargetUserId == cmd.Follow.TargetUserId);
		if (userFollow == null)
		{
			userFollow = new()
			{
				SourceUserId = cmd.UserId.Value,
				TargetUserId = cmd.Follow.TargetUserId,
			};
			await Repo.Save(userFollow);
		}
		else
		{
			Repo.Remove(userFollow);
			await Repo.SaveChanges();
		}

		var article = await Repo.GetArticleBySlug(cmd.Follow.Slug);
        if (article == null)
        {
			cmd.Result = UiBuilder.RenderPage(ErrorPage("Article not found", null));
			return;
		}

		var page1 =
		FollowCounter(article, Targets.FollowCounter1.Id, false, true);

		var page2 =
		FollowCounter(article, Targets.FollowCounter2.Id, true, true);

		cmd.Result = UiBuilder.RenderPages(page1, [page2]);

	}
}

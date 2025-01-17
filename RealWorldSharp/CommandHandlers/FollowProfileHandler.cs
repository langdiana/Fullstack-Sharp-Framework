namespace RealWorldSharp.CommandHandlers;

public class FollowProfileHandler : CommandHandlerBase<FollowProfileCommand>
{
	public override async Task Execute(FollowProfileCommand cmd)
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

		var profile = await Repo.GetProfileModelShort(cmd.Follow.Username );
		if (profile == null)
		{
			cmd.Result = UiBuilder.RenderPage(ErrorPage("Profile not found", null));
			cmd.ErrorMessage = "Profile not found";
			return;
		}

		var page =
		FollowCounterProfile(profile, Targets.FollowCounterProfile.Id, true);

		cmd.Result = UiBuilder.RenderPage(page);

	}
}


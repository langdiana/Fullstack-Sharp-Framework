namespace RealWorldSharp.CommandHandlers;

public class GetProfilePageHandler : CommandHandlerBase<GetProfilePageCommand>
{

	public override async Task Execute(GetProfilePageCommand cmd)
	{
		var profile = await Repo.GetProfileModel(cmd.Username, cmd.IsMyArticles);
		if (profile == null)
		{
			cmd.Result = UiBuilder.RenderPage(ErrorPage("Profile not found", null));
			return;
		}

		var page = ProfilePage(profile, cmd.IsMyArticles);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

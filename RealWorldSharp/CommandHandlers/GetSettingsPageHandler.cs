namespace RealWorldSharp.CommandHandlers;

public class GetSettingsPageHandler : CommandHandlerBase<GetSettingsPageCommand>
{

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public override async Task Execute(GetSettingsPageCommand cmd)
	{
		var userModel = Repo.GetCrtUserModel();
		if (userModel == null)
			return;

		var page = SettingsPage(userModel, null, null, null);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

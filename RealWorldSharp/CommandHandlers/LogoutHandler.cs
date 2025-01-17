using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.CommandHandlers;

public class LogoutHandler : CommandHandlerBase<LogoutCommand>
{

	public override async Task Execute(LogoutCommand cmd)
	{
		await AuthService.SignOut();
		await Repo.SetCrtUser(null);

		var home = await GetHomePage(FeedTypeEnum.Global);
		var header = HeaderUnauth();
		cmd.Result = UiBuilder.RenderPages(home, [header]);
	}
}

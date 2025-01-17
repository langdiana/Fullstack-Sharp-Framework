namespace RealWorldSharp.CommandHandlers;

public class GetLoginPageHandler : CommandHandlerBase<GetLoginPageCommand>
{

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public override async Task Execute(GetLoginPageCommand cmd)
	{
		var login = new LoginModel();
		var page = LoginPage(login, true);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

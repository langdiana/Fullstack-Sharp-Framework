namespace RealWorldSharp.CommandHandlers;

public class GetRegisterPageHandler : CommandHandlerBase<GetRegisterPageCommand>
{

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public override async Task Execute(GetRegisterPageCommand cmd)
	{
		var register = new RegisterModel();
		var page = RegisterPage(register, null, null, null);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

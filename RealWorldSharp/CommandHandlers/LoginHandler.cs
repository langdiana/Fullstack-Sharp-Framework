using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.CommandHandlers;

public class LoginHandler : CommandHandlerBase<LoginCommand>
{

	public override async Task Execute(LoginCommand cmd)
	{

		string? errorMessage = null;

		if (string.IsNullOrWhiteSpace(cmd.Login.Email) || cmd.Login.Password == null || cmd.Login.Password.Length < 3)
		{
			errorMessage = "Invalid credentials";
			var page = LoginPage(cmd.Login, false, errorMessage);
			cmd.Result = UiBuilder.RenderPage(page);
			cmd.ErrorMessage = errorMessage;
			return;
		}

		var user = await Repo.FirstOrDefault<User>(x => x.Email == cmd.Login.Email && x.Password == cmd.Login.Password);
		if (user == null)
		{
			errorMessage = "User not found";
			var page = LoginPage(cmd.Login, false, errorMessage);
			cmd.Result = UiBuilder.RenderPage(page);
			cmd.ErrorMessage = errorMessage;
			return;
		}

		await AuthService.SignIn(user);
		await Repo.SetCrtUser(user.UserId);

		var home = await GetHomePage(FeedTypeEnum.Global);
		var header = HeaderAuth(user.Username, true);
		cmd.Result = UiBuilder.RenderPages(home, [header]);
	}
}


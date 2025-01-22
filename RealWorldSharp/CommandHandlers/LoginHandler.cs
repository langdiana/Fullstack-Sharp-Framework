using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.CommandHandlers;

public class LoginHandler : CommandHandlerBase<LoginCommand>
{

	public override async Task Execute(LoginCommand cmd)
	{
		// check password
		if (cmd.Login.Password == null || cmd.Login.Password.Length < 3)
		{
			var page = LoginPage(cmd.Login, false);
			cmd.Result = UiBuilder.RenderPage(page);
			cmd.ErrorMessage = "Invalid credentials";
			return;
		}

		var user = await Repo.FirstOrDefault<User>(x => x.Email == cmd.Login.Email);
		//var user = await Repo.FirstOrDefault<User>(x => x.Email == x.Email);
		if (user == null)
		{
			var page = LoginPage(cmd.Login, false);
			cmd.Result = UiBuilder.RenderPage(page);
			cmd.ErrorMessage = "User not found";
			return;
		}

		await AuthService.SignIn(user);
		await Repo.SetCrtUser(user.UserId);

		var home = await GetHomePage(FeedTypeEnum.Global);
		var header = HeaderAuth(user.Username);
		cmd.Result = UiBuilder.RenderPages(home, [header]);
	}
}


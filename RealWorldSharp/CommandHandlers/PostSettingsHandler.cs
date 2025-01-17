using RealWorldSharp.Data.Models;

namespace RealWorldSharp.CommandHandlers;

public class PostSettingsHandler : CommandHandlerBase<PostSettingsCommand>
{
	public override async Task Execute(PostSettingsCommand cmd)
	{

		User? user = null;
		string? usernameError = null;
		string? emailError = null;
		string? passwordError = null;
		if (string.IsNullOrWhiteSpace(cmd.User.Password) || cmd.User.Password.Length < 3)
		{
			passwordError = "Password must be min 3 characters";
			cmd.AddError(passwordError);
		}

		if (string.IsNullOrWhiteSpace(cmd.User.Email))
		{
			emailError = "Email must not be empty";
			cmd.AddError(emailError);
		}
		else
		{
			user = await Repo.FirstOrDefault<User>(x => x.Email == cmd.User.Email && x.UserId != cmd.User.UserId);
			if (user != null)
			{
				emailError = "This Email was already taken";
				cmd.AddError(emailError);
			}
		}

		if (string.IsNullOrWhiteSpace(cmd.User.Username))
		{
			usernameError = "Username must not be empty";
			cmd.AddError(usernameError);
		}
		else
		{
			user = await Repo.FirstOrDefault<User>(x => x.Username == cmd.User.Username && x.UserId != cmd.User.UserId);
			if (user != null)
			{
				usernameError = "This Username was already taken";
				cmd.AddError(usernameError);
			}
		}

		if (passwordError != null || emailError != null || usernameError != null)
		{
			var page = SettingsPage(cmd.User, usernameError, emailError, passwordError);
			cmd.Result = UiBuilder.RenderPage(page);
			return;
		}

		user = cmd.User.ToEntity();
		await Repo.Save(user);

		var home = await GetHomePage(FeedTypeEnum.Global);
		var header = HeaderAuth(user.Username);
		cmd.Result = UiBuilder.RenderPages(home, [header]);
	}
}

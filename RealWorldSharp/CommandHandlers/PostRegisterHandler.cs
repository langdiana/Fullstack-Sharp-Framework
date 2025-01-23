namespace RealWorldSharp.CommandHandlers;

public class PostRegisterHandler : CommandHandlerBase<PostRegisterCommand>
{

	public override async Task Execute(PostRegisterCommand cmd)
	{
		User? user = null;
		string? usernameError = null;
		string? emailError = null;
		string? passwordError = null;
		if (string.IsNullOrWhiteSpace(cmd.Register.Password) || cmd.Register.Password.Length < 3)
		{
			passwordError = "Password must be min 3 characters";
			cmd.AddError(passwordError);
		}

		if (string.IsNullOrWhiteSpace(cmd.Register.Email))
		{
			emailError = "Email must not be empty";
			cmd.AddError(emailError);
		}
		else
		{
			user = await Repo.FirstOrDefault<User>(x => x.Email == cmd.Register.Email);
			if (user != null)
			{
				emailError = "This Email was already taken";
				cmd.AddError(emailError);
			}
		}

		if (string.IsNullOrWhiteSpace(cmd.Register.Username))
		{
			usernameError = "Username must not be empty";
			cmd.AddError(usernameError);
		}
		else
		{
			user = await Repo.FirstOrDefault<User>(x => x.Username == cmd.Register.Username);
			if (user != null)
			{
				usernameError = "This Username was already taken";
				cmd.AddError(usernameError);
			}
		}

		if (passwordError != null || emailError != null || usernameError != null)
		{
			var page = RegisterPage(cmd.Register, usernameError, emailError, passwordError);
			cmd.Result = UiBuilder.RenderPage(page);
			return;
		}

		user = new()
		{
			Username = cmd.Register.Username!,
			Email = cmd.Register.Email!,
			Password = cmd.Register.Password!,
		};

		await Repo.Save(user);

		await AuthService.SignIn(user);
		await Repo.SetCrtUser(user.UserId);

		var userModel = Repo.GetCrtUserModel();
		if (userModel == null)
			return;

		var settingPage = SettingsPage(userModel, null, null, null);
		var header = HeaderAuth(user.Username, true);
		cmd.Result = UiBuilder.RenderPages(settingPage, [header]);
	}
}


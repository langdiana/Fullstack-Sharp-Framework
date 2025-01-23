namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement LoginPage(LoginModel login, bool isValid, string? errorMessage)
	{
		var js = new JSBuilder<LoginModel>(login);
		var xdata = js.Build();

		return
		div(new() { className = "auth-page", xData = xdata },
			div(new() { className = "container page" },
				div(new() { className = "row" },
					div(new() { className = "col-md-6 offset-md-3 col-xs-12" },
						h1(new() { className = "text-xs-center" }, "Sign in"
						),
						p(new() { className = "text-xs-center" },
							a(new() { href = $"{Routes.Register}" }, "Need an account?")
						),
						isValid ? Frag() : ul(new() { className = "error-messages" },
							li(_, errorMessage ?? ""
							)
						),
						form(new() { hxBoost = "false" },
							fieldset(new() { className = "form-group" },
								input(new() { className = "form-control form-control-lg", htype = "text", placeholder = "Email", xModel = js.Field(x => x.Email) }
								)
							),
							fieldset(new() { className = "form-group" },
								input(new() { className = "form-control form-control-lg", htype = "password", placeholder = "Password", xModel = js.Field(x => x.Password) }
								)
							),
							button(new() { className = "btn btn-lg btn-primary pull-xs-right", hxPost = Routes.Login, hxTargetInner = Targets.MainId.Target, hxBindVals = js.JsonData, hxPushUrl = Routes.Home }, "Sign in"
							)
						)
					)
				)
			)
		);

	}
}

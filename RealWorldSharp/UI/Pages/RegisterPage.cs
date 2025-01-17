namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement RegisterPage(RegisterModel register, string? usernameError, string? emailError, string? passwordError)
	{
		var js = new JSBuilder<RegisterModel>(register);
		var xdata = js.Build();

		return

		div(new() { className = "auth-page", xData = xdata },
			div(new() { className = "container page" },
				div(new() { className = "row" },
					div(new() { className = "col-md-6 offset-md-3 col-xs-12" },
						h1(new() { className = "text-xs-center" }, "Sign up"
						),
						p(new() { className = "text-xs-center" },
							a(new() { href = $"{Routes.Login}" }, "Have an account?")
						),
						form(new() { hxBoost = "false" },
							usernameError == null ? Frag() : ul(new() { className = "error-messages" },
								li(_, usernameError
								)
							),
							fieldset(new() { className = "form-group" },
								input(new() { className = "form-control form-control-lg", htype = "text", placeholder = "Username", xModel = js.Field(x => x.Username) }
								)
							),
							fieldset(new() { className = "form-group" },
								emailError == null ? Frag() : ul(new() { className = "error-messages" },
									li(_, emailError
									)
								),
								input(new() { className = "form-control form-control-lg", htype = "text", placeholder = "Email", xModel = js.Field(x => x.Email) }
								)
							),
							fieldset(new() { className = "form-group" },
								passwordError == null? Frag() : ul(new() { className = "error-messages" },
									li(_, passwordError
									)
								),
								input(new() { className = "form-control form-control-lg", htype = "password", placeholder = "Password", xModel = js.Field(x => x.Password) }
								)
							),
							button(new() { className = "btn btn-lg btn-primary pull-xs-right", hxPost = Routes.Register, hxTargetInner = Targets.MainId.Target, hxBindVals = js.JsonData }, "Sign up"
							)
						)
					)
				)
			)
		);
	}

}
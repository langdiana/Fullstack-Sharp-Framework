using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement SettingsPage(UserModel user, string? usernameError, string? emailError, string? passwordError)
	{
		var js = new JSBuilder<UserModel>(user);
		var xdata = js.Build();


		return
		div(new() { className = "settings-page", hxBoost = "false" },
			div(new() { className = "container page" },
				div(new() { className = "row" },
					div(new() { className = "col-md-6 offset-md-3 col-xs-12" },
						h1(new() { className = "text-xs-center" }, "Your Settings"
						),
						form(new() { xData = xdata },
							fieldset(_,
								fieldset(new() { className = "form-group" },
									input(new() { className = "form-control", htype = "text", placeholder = "URL of profile picture", xModel = js.Field(x => x.Image) }
									)
								),
								fieldset(new() { className = "form-group" },
									usernameError == null ? Frag() : ul(new() { className = "error-messages" },
										li(_, usernameError
										)
									),
									input(new() { className = "form-control form-control-lg", htype = "text", placeholder = "Your Name", xModel = js.Field(x => x.Username) }
									)
								),
								fieldset(new() { className = "form-group" },
									textarea(new() { className = "form-control form-control-lg", rows = "8", placeholder = "Short bio about you", xModel = js.Field(x => x.Bio) }
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
									passwordError == null ? Frag() : ul(new() { className = "error-messages" },
										li(_, passwordError
										)
									),
									input(new() { className = "form-control form-control-lg", htype = "password", placeholder = "New Password", xModel = js.Field(x => x.Password) }
									)
								),
								button(new() { className = "btn btn-lg btn-primary pull-xs-right", hxPost = Routes.Settings, hxTargetInner = Targets.MainId.Target, hxBindVals = js.JsonData }, "Update Settings"
								)
							)
						),
						hr(),
						button(new() { className = "btn btn-outline-danger", hxGet = Routes.Logout,	hxSwap = "innerHTML", hxTarget = Targets.MainId.Target }, "Or click here to logout."
						)
					)
				)
			)
		);
	}

}
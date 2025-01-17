namespace RealWorldSharp.UI;

public static partial class Layout
{
	public static HtmlElement HeaderAuth(string username)
	{
		return
		nav(new() { className = "navbar navbar-light", id = Targets.Header.Id, hxOob = true },
			div(new() { className = "container" },
				a(new() { className = "navbar-brand", href = "/" }, "conduit"),
				ul(new() { className = "nav navbar-nav pull-xs-right" },
					li(new() { className = "nav-item" },
						a(new() { className = "nav-link active", href = Routes.Home }, "Home")
					),
					li(new() { className = "nav-item" },
						a(new() { className = "nav-link", href = Routes.Editor }, i(new() { className = "ion-compose" }), "&nbsp;New Article")
					),
					li(new() { className = "nav-item" },
						a(new() { className = "nav-link", href = Routes.Settings }, i(new() { className = "ion-gear-a" }), "&nbsp;Settings")
					),
					li(new() { className = "nav-item" },
						a(new() { className = "nav-link", href = $"{Routes.Profile}{username}" }, img(new() { src = "", className = "user-pic" }), $"{username}")
					),
					li(new() { className = "nav-item", },
						a(new() { className = "nav-link", href = Routes.Logout, }, "Sign out"
						)
					)
				)
			)
		);
	}

}

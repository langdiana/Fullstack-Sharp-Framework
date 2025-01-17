namespace RealWorldSharp.UI;

public static partial class Layout
{
	public static HtmlElement HeaderUnauth()
	{
		return
		nav(new() { className = "navbar navbar-light", id = Targets.Header.Id, hxOob = true },
			div(new() { className = "container", },
				a(new() { className = "navbar-brand", href = "/", }, "conduit"
				),
				ul(new() { className = "nav navbar-nav pull-xs-right", },
					li(new() { className = "nav-item", },
						a(new() { className = "nav-link active", href = Routes.Home }, "Home"
						)
					),
					li(new() { className = "nav-item", },
						a(new() { className = "nav-link", href = Routes.Login, }, "Sign in"
						)
					),
					li(new() { className = "nav-item", },
						a(new() { className = "nav-link", href = Routes.Register, }, "Sign up"
						)
					)
				)
			)
		);


	}

}

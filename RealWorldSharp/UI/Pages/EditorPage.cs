using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement EditorPage(ArticleModel article, string? errorMessage)
	{

		var js = new JSBuilder<ArticleModel>(article);
		var xdata = js.Build();

		bool isValid = errorMessage == null;

		string tagScript = @"
{
crtitem: null,
pushItem() { if (this.crtitem) {this.item.Tags.push(this.crtitem); this.crtitem = null} },
removeItem(tag) { if (this.item.Tags.indexOf(tag) > -1) {this.item.Tags.splice(this.item.Tags.indexOf(tag),1)} }
}
";

		return
		div(new() { className = "editor-page", hxBoost = "false" },
			div(new() { className = "container page" },
				div(new() { className = "row" },
					div(new() { className = "col-md-10 offset-md-1 col-xs-12" },

						form(new() { xData = xdata },
							fieldset(_,
								fieldset(new() { className = "form-group" },
									input(new() { htype = "text", className = "form-control form-control-lg", placeholder = "Article Title", xModel = js.Field(x => x.Title) }
									)
								),
								fieldset(new() { className = "form-group" },
									input(new() { htype = "text", className = "form-control", placeholder = "What's this article about?", xModel = js.Field(x => x.Description) }
									)
								),
								fieldset(new() { className = "form-group" },
									textarea(new() { className = "form-control", rows = "8", placeholder = "Write your article (in markdown)", xModel = js.Field(x => x.Body) }
									)
								),
								fieldset(new() { className = "form-group", xData = tagScript, xClickOutside = "pushItem()", xKeyupEnter = "pushItem()" },
									input(new() { htype = "text", className = "form-control", placeholder = "Enter tags", xModel = "crtitem" }
									),
									div(new() { className = "tag-list" },									
										template(new() { xFor = "tag in item.Tags", xKey = "tag", },
											span(new() { className = "tag-default tag-pill", xClick = "removeItem(tag)" }, i(new() { className = "ion-close-round" }), span(new() { xText = "tag" }))
										)										
									)
								),

								isValid ? Frag() : ul(new() { className = "error-messages" },
									li(_, $"{errorMessage}"
									)
								),

								button(new() { className = "btn btn-lg pull-xs-right btn-primary", hxPost = Routes.Editor, hxTargetInner = Targets.MainId.Target, hxBindVals = js.JsonData }, "Publish Article"
								)
							)
						)

					)
				)
			)
		);
	}

}
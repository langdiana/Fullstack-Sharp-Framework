namespace RealWorldSharp.UI.Pages;

public static partial class Pages
{
	public static HtmlElement ArticlePage(ArticleModel article)
	{
		var js = new JSBuilder<ArticleModel>(article);
		var xdata = js.Build();

		PostCommentModel comment = new() {ArticleId = article.ArticleId };
		var jsComment = new JSBuilder<PostCommentModel>(comment);
		var xdataComment = jsComment.Build();

		var editorLink = $"{Routes.Editor}{article.Slug}";
		var profileLink = $"{Routes.Profile}{article.Author.Username}";
		var articleDeleteLink = $"{Routes.ArticleDelete}{article.ArticleId}";

		return
		div(new() { className = "article-page", xData = xdata, hxBoost = "false",
		},
			div(new() { className = "banner" },
				div(new() { className = "container" },
					h1(_, article.Title
					),
					div(new() { className = "article-meta" },
						a(new() { href = profileLink },
							img(new() { src = article.Author.Image })
						),
						div(new() { className = "info" },
							a(new() { href = profileLink, className = "author" }, article.Author.Username),
							span(new() { className = "date" }, $"{article.CreatedAt}")
						),
											
						article.IsAuthor ? Frag() : FollowCounter(article, Targets.FollowCounter1.Id, false, article.CrtUser != null),
						"&nbsp;&nbsp;",
						article.IsAuthor ? Frag() : FavoriteCounterView(Targets.FavCounterView1.Id, article, false, article.CrtUser != null),

						!article.IsAuthor ? Frag() : button(new() { className = "btn btn-sm btn-outline-secondary", hxGet = editorLink, hxTargetInner = Targets.MainId.Target, hxPushUrl = "true" },
							i(new() { className = "ion-edit" }), "Edit Article"
						),
						!article.IsAuthor ? Frag() : button(new() { className = "btn btn-sm btn-outline-danger", hxDelete = articleDeleteLink, hxTarget = Targets.MainId.Target },
							i(new() { className = "ion-trash-a" }), "Delete Article"
						)
					)
				)
			),
			div(new() { className = "container page" },
				div(new() { className = "row article-content" },
					div(new() { className = "col-md-12" },
						p(new() { xHtml = js.Field(x => x.HtmlBody) }
						),
						ArticleTags(article.Tags)
					)
				),

				hr(),

				div(new() { className = "article-actions" },
					div(new() { className = "article-meta" },
						a(new() { href = profileLink },
							img(new() { src = article.Author.Image })
						),
						div(new() { className = "info" },
							a(new() { href = profileLink, className = "author" }, article.Author.Username),
							span(new() { className = "date" }, $"{article.CreatedAt}")
						),

						article.IsAuthor ? Frag() : FollowCounter(article, Targets.FollowCounter2.Id, false, article.CrtUser != null),
						"&nbsp;",
						article.IsAuthor ? Frag() : FavoriteCounterView(Targets.FavCounterView2.Id, article, false, article.CrtUser != null),

						!article.IsAuthor ? Frag() : button(new() { className = "btn btn-sm btn-outline-secondary", hxGet = editorLink, hxTargetInner = Targets.MainId.Target, hxPushUrl = "true" },
							i(new() { className = "ion-edit" }), "Edit Article"
						),
						!article.IsAuthor ? Frag() : button(new() { className = "btn btn-sm btn-outline-danger", hxDelete = articleDeleteLink, hxTarget = Targets.MainId.Target },
							i(new() { className = "ion-trash-a" }), "Delete Article"
						)
					)
				),
				div(new() { className = "row" },
					div(new() { className = "col-xs-12 col-md-8 offset-md-2" },
						form(new() { className = "card comment-form", xData = xdataComment },
							div(new() { className = "card-block" },
								textarea(new() { className = "form-control", placeholder = "Write a comment...", rows = "3", xModel = jsComment.Field(x => x.Body) }
								)
							),
							div(new() { className = "card-footer" },
								img(new() { src = article.CrtUser?.Image, className = "comment-author-img" }),
								button(new() { className = "btn btn-sm btn-primary", hxPost = Routes.PostComment, hxTargetInner = Targets.MainId.Target, hxBindVals = js.JsonData }, "Post Comment"
								)
							)
						),
						Comments(article.Comments)
					)
				)
			)
		);
	}

	static HtmlElement ArticleTags(List<string> tags)
	{
		var root = Frag();

		foreach (var tag in tags)
		{
			var itemElem =
			span(new() { className = "tag-default tag-pill" }, tag);

			root.Add(itemElem);
		}

		return root;
	}

	static HtmlElement Comments(List<Comment> comments)
	{
		string getDeleteModel(Comment comment)
		{
			DeleteCommentModel del = new() { CommentId = comment.CommentId, ArticleId = comment.ArticleId};
			return JsonConvert.SerializeObject(del);
		}

		var root = Frag();

		foreach (var comment in comments)
		{
			var profileLink = $"{Routes.Profile}{comment.Author.Username}";

			var itemElem =
			div(new() { className = "card" },
				div(new() { className = "card-block" },
					p(new() { className = "card-text" }, comment.Body
					)
				),
				div(new() { className = "card-footer" },
					a(new() { href = profileLink, className = "comment-author" },
						img(new() { src = comment.Author.Image, className = "comment-author-img" })
					), "&nbsp;",
					a(new() { href = profileLink, className = "comment-author" }, comment.Author.Username),
					span(new() { className = "date-posted" }, $"{comment.CreatedAt}"),
					span(new() { className = "mod-options", hxPost = Routes.DeleteComment, hxTrigger = "click", jsonVals = getDeleteModel(comment), hxTarget = Targets.MainId.Target }, 
						i(new() { className = "ion-trash-a" }))
				)
			);

			root.Add(itemElem);
		}

		return root;
	}

}



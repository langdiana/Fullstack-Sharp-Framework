namespace RealWorldSharp.UI.Components
{
	public static partial class Compo
	{
		public static HtmlElement ArticleList(List<ArticleModel> articles, PagerInfo pagerInfo, bool isAuthenticated)
		{

			var root =
			div(new() { id = Targets.PageCounter.Id });

			foreach (var article in articles)
			{
				var profileLink = $"{Routes.Profile}{article.Author.Username}";
				var articleLink = $"{Routes.Article}{article.Slug}";

				var itemElem =
				div(new() { className = "article-preview" },
					div(new() { className = "article-meta" },
						a(new() { href = profileLink },
							img(new() { src = article.Author.Image })
						),
						div(new() { className = "info" },
							a(new() { href = profileLink, className = "author" }, article.Author.Username ),
							span(new() { className = "date" }, $"{article.CreatedAt}")
						),
						FavoriteCounter(article, isAuthenticated)
					),
					a(new() { href = articleLink, className = "preview-link" },
						h1(_, article.Title
						),
						p(_, article.Description
						),
						span(_, "Read more..."),
						ul(new() { className = "tag-list" },
							TagList(article.Tags)
						)
					)
				);

				root.Add(itemElem);
			}

			root.Add(
			Pager(pagerInfo)
			);		

			return root;
		}

		static HtmlElement TagList(List<string> tags)
		{
			var root =
			Frag();

			foreach (var tag in tags)
			{
				var itemElem =
				li(new() { className = "tag-default tag-pill tag-outline"}, tag);

				root.Add(itemElem);
			}

			return root;
		}

		static HtmlElement Pager(PagerInfo pagerInfo)
		{
			var root =
			ul(new() { className = "pagination" });

			if (pagerInfo.ItemCount > 0 && pagerInfo.RowsPerPage > 0)
			{
				int pageCount = (pagerInfo.ItemCount - 1) / pagerInfo.RowsPerPage + 1;
				for (int k = 1; k <= pageCount; k++)
				{
					string className;
					CustomAttributes attr;

					if (k == pagerInfo.PageNumber)
					{
						className = "page-item active";
						attr = new() { className = "page-link" };
					}
					else
					{
						className = "page-item";
						attr = new()
						{
							className = "page-link",
							hxPostRef = pagerInfo.GetRoute(),
							jsonVals = pagerInfo.GetJson(k),
							hxTarget = Targets.PageCounter.Target,
							hxSwap = "outerHTML",
						};
					}

					var itemElem =
					li(new() { className = className },
						a(attr, $"{k}")
					);

					root.Add(itemElem);
				}
			}

			return root;
		}

		// not used, proof of concept
		public static HtmlElement ArticleListAlternate(List<ArticleModel> articles, int? userId = null)
		{
			var js = new JSBuilder<ArticleModel>(articles);
			var xdata = js.Build();
			var profileLink = $"'{Routes.Profile}' + {js.Field(x => x.Author.Username)}";
			var articleLink = $"'{Routes.Article}' + {js.Field(x => x.Slug)}";

			return
			template(new() { xData = xdata, xFor = js.ForItem, xKey = js.Field(x => x.ArticleId) },
				div(new() { className = "article-preview" },
					div(new() { className = "article-meta" },
						a(new() { xBindRef = profileLink },
							img(new() { xBindSrc = js.Field(x => x.Author.Image) })
						),
						div(new() { className = "info" },
							a(new() { xBindRef = profileLink, className = "author", xText = js.Field(x => x.Author.Username) }),
							span(new() { className = "date" }, "January 20th")
						),
						button(new() { className = "btn btn-outline-primary btn-sm pull-xs-right" },
							i(new() { className = "ion-heart", xText = js.Field(x => x.FavoritesCount) })
						)
					),
					a(new() { xBindRef = articleLink, className = "preview-link" },
						h1(new() { xText = js.Field(x => x.Title) }
						),
						p(new() { xText = js.Field(x => x.Description) }
						),
						span(_, "Read more..."),
						ul(new() { className = "tag-list"},
							template(new() { xData = xdata, xFor = "tag in item.Tags", xKey = "tag.TagId", },
								li(new() { className = "tag-default tag-pill tag-outline", xText = "tag.TagName" })
							)
						)
					)
				)
			);


		}
	}

}

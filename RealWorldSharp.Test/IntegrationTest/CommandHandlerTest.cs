using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealWorldSharp.CommandHandlers;
using RealWorldSharp.Commands;
using RealWorldSharp.Data;
using RealWorldSharp.Data.Entities;
using RealWorldSharp.Data.Models;
using RealWorldSharp.Interfaces;
using RealWorldSharp.Repos;
using SharpHtml;
using static RealWorldSharp.Test.TestUtils;
using static SharpHtml.Html;

namespace RealWorldSharp.Test.IntegrationTest;

[TestClass]
public class CommandHandlerTest
{
	void CreateContext()
	{
		context = new RealWorldContext(
			new DbContextOptionsBuilder<RealWorldContext>()
			.UseInMemoryDatabase("RealWorldDB")
			.Options
			);
	}

	public class FakeUIBuilder : IUIBuilder
	{
		public IResult RenderPage(HtmlElement page)
		{
			return Results.Content(Frag().Render(), contentType: "text/html");
		}

		public IResult RenderPages(HtmlElement page, List<HtmlElement> oobPages)
		{
			return Results.Content(Frag().Render(), contentType: "text/html");
		}
	}


	IRealWorldRepo repo;
	RealWorldContext context = null!;
	public IUIBuilder uiBuilder = null!;
	public IAuthService authService = null!;

	public CommandHandlerTest()
	{
		CreateContext();
		CancellationToken cancellationToken = new CancellationToken();
		repo = new RealWorldRepo(context, cancellationToken);
		uiBuilder = new FakeUIBuilder();
		authService = new FakeAuthService();
	}

	[TestMethod]
	public async Task PostArticle()
	{
		var title = Guid.NewGuid().ToString();
		var cmd = new PostArticleCommand { UserId = 1, Article = new ArticleModel
				{
					Title = title,
					Description = "Descr",
					Body = "Body",
				}
		};
		PostArticleHandler handler = new() { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
		await handler.Execute(cmd);
		Assert.IsTrue(cmd.OK, cmd.ErrorMessage);
		
		var article = await repo.FirstOrDefault<Article>(x => x.Title == title);
		Assert.IsNotNull(article);
	}

	[TestMethod]
	public async Task DeleteArticle()
	{
		Article article = new Article
		{
			Title = "Title",
			Description = "Descr",
			Body = "Body",
			Slug = "Slug",
			UserId = 1,
		};

		await repo.Save(article);	

		var cmd = new DeleteArticleCommand { ArticleId = article.ArticleId };
		DeleteArticleHandler handler = new() { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
		await handler.Execute(cmd);
		Assert.IsTrue(cmd.OK, cmd.ErrorMessage);

		var deletedArticle = await repo.FirstOrDefault<Article>(x => x.ArticleId == article.ArticleId);
		Assert.IsNull(deletedArticle);
	}

}

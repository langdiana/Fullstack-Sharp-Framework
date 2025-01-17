using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealWorldSharp.CommandHandlers;
using RealWorldSharp.Commands;
using RealWorldSharp.Data;
using RealWorldSharp.Data.Entities;
using RealWorldSharp.Interfaces;
using RealWorldSharp.Repos;
using RealWorldSharp.UI;
using SharpHtml;
using System.Diagnostics;
using static RealWorldSharp.Test.TestUtils;

namespace RealWorldSharp.Test
{
	[TestClass]
	public class PageTest
	{

		void CreateContext()
		{
			context = new RealWorldContext(
				new DbContextOptionsBuilder<RealWorldContext>()
				.UseInMemoryDatabase("RealWorldDB")
				.Options
				);
		}

		IRealWorldRepo repo;
		RealWorldContext context = null!;
		public IUIBuilder uiBuilder = null!;
		public IAuthService authService = null!;

		public PageTest()
		{
			CreateContext();
			Utils.SeedContext(context);
			CancellationToken cancellationToken = new CancellationToken();
			repo = new RealWorldRepo(context, cancellationToken);
			uiBuilder = new FakeUIBuilder(false, "test");
			authService = new FakeAuthService();
		}

		void ShowHtml(IResult result)
		{
			var html = ((Microsoft.AspNetCore.Http.HttpResults.ContentHttpResult)result).ResponseContent;
			var fileName = @"c:\temp\RealWorldSharp\TestHtml\page.html"; // make sure you copy local css files (if any) in this folder
			File.WriteAllText(fileName, html);
			RunBrowser(fileName);
		}

		[TestMethod]
		public async Task GetHomePage()
		{
			var cmd = new GetHomePageCommand { };
			GetHomePageHandler handler = new () { Repo = repo, UiBuilder = uiBuilder, AuthService = authService};
			await handler.Execute(cmd);
			ShowHtml(cmd.Result);
		}

		[TestMethod]
		public async Task GetArticlePage()
		{
			var article = await repo.FirstOrDefault<Article>(x => x.UserId == 1);
			var cmd = new GetArticlePageCommand { Slug = article!.Slug };
			GetArticlePageHandler handler = new () { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
			await handler.Execute(cmd);
			ShowHtml(cmd.Result);
		}

		[TestMethod]
		public async Task GetFavoriteButton()
		{
			var article = await repo.FirstOrDefault<Article>(x => x.UserId == 1);
			var cmd = new FavoriteCommand { IsView = false, Favorite = new() { ArticleId = article!.ArticleId} };
			FavoriteHandler handler = new() { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
			await handler.Execute(cmd);
			ShowHtml(cmd.Result);
		}

	}
}
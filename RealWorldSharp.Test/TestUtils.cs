using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealWorldSharp.Commands;
using RealWorldSharp.Data.Entities;
using RealWorldSharp.Data.Models;
using RealWorldSharp.Enums;
using RealWorldSharp.Interfaces;
using RealWorldSharp.UI;
using SharpHtml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldSharp.Test;

internal class TestUtils
{
	public static void RunBrowser(string fileName)
	{
		var p = new Process();
		p.StartInfo = new ProcessStartInfo(fileName)
		{
			UseShellExecute = true
		};
		p.Start();
	}

	public class HtmlTest
	{
		HtmlDocument htmlDoc;

		public HtmlTest(string html)
		{
			htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(html);
		}

		public string? GetRootClass()
		{
			return htmlDoc.DocumentNode.FirstChild.Attributes["class"].Value;
		}

		public bool HasText(string elemName, string text)
		{
			var node = htmlDoc.DocumentNode.Descendants(elemName).FirstOrDefault(x => x.InnerText.Trim() == text);
			return node != null;
		}

	}

	public class FakeUIBuilder : UIBuilder
	{
		public FakeUIBuilder(bool isHtmx, string? username) : base(isHtmx, username)
		{
		}

		protected override IResult RenderApp(HtmlElement mainContent)
		{
			var appHead = new AppHead();
			var app = appHead.Build();
			app.Add(mainContent);
			var html = app.Render();
			return Results.Content(html, contentType: "text/html");
		}
	}

	public class FakeAuthService : IAuthService
	{
		public Task SignIn(User user)
		{
			return Task.CompletedTask;
		}

		public Task SignOut()
		{
			return Task.CompletedTask;
		}
	}

	public class FakeRepo : IRealWorldRepo
	{
		public FakeRepo(User? crtUser)
		{
			CrtUser = crtUser;
			if (crtUser != null)
				CrtUserId = crtUser.UserId;
		}

		User? CrtUser;
		int? CrtUserId;

		public void Add<TEntity>(TEntity entity) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public void ClearChangeTracker()
		{
			throw new NotImplementedException();
		}

		public Task DeleteArticle(int articleId)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity?> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task<List<TEntity>> GetAll<TEntity>() where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task<ArticleModel?> GetArticleById(int articleId)
		{
			throw new NotImplementedException();
		}

		public Task<ArticleModel?> GetArticleBySlug(string slug)
		{
			throw new NotImplementedException();
		}

		public Task<List<ArticleModel>> GetArticles(int pageNumber)
		{
			throw new NotImplementedException();
		}

		public Task<List<ArticleModel>> GetArticlesByAuthor(int userId)
		{
			throw new NotImplementedException();
		}

		List<ArticleModel> GetArticles()
		{

			List<ArticleModel> articles =
			[
				new(){ ArticleId = 1, Author = new(){UserId = 1, Username = "Author" }, }
			];

			if (CrtUser != null)
			{
				foreach (var article in articles)
				{
					if (article.ArticleFavorites.Any(x => x.UserId == CrtUser.UserId))
						article.Favorited = true;
					article.CrtUser = CrtUser;
				}
			}

			return articles;
		}

		public async Task GetArticlesForHome(GetArticlesForHomeCommand cmdRepo)
		{
			/*
			var linq = getArticleModelsLinq();
			switch (cmdRepo.PagerInfo.FeedType)
			{
				case FeedTypeEnum.Yours:
					linq = linq.Where(x => x.Author.UserId == CrtUserId);
					break;
				case FeedTypeEnum.Global:
					break;
				case FeedTypeEnum.Tag:
					linq = linq.Where(x => x.Tags.Any(y => y == cmdRepo.PagerInfo.Tag));
					break;
			}
			*/

			cmdRepo.Articles = GetArticles();
			cmdRepo.PagerInfo.ItemCount = 10;
			cmdRepo.PagerInfo.RowsPerPage = RowsPerPage;
		}

		public Task GetArticlesForProfile(GetArticlesForProfileCommand cmd)
		{
			throw new NotImplementedException();
		}

		public UserModel? GetCrtUserModel()
		{
			throw new NotImplementedException();
		}

		int RowsPerPage = 5;

		public async Task<HomeModel> GetHomeModel(FeedTypeEnum feedType, string? tag = null)
		{
			HomeModel hm = new();
			hm.CrtUser = CrtUser;

			GetArticlesForHomeCommand cmdRepo = new();
			cmdRepo.PagerInfo.FeedType = feedType;
			cmdRepo.PagerInfo.RowsPerPage = RowsPerPage;
			cmdRepo.PagerInfo.PageNumber = 1;
			cmdRepo.PagerInfo.Tag = tag;

			await GetArticlesForHome(cmdRepo);

			hm.Articles = cmdRepo.Articles;
			hm.PagerInfo = cmdRepo.PagerInfo;

			//hm.Tags = await context.Tags.Take(10).ToListAsync(); // TODO: order by name count

			return hm;
		}

		public Task<ProfileModel?> GetProfileModel(string username, bool isMyArticles)
		{
			throw new NotImplementedException();
		}

		public Task<ProfileModel?> GetProfileModelShort(string username)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetUserById(int userId)
		{
			throw new NotImplementedException();
		}

		public void Remove<TEntity>(TEntity entity) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public void RemoveRange<TEntity>(List<TEntity> entityList) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public void RemoveRange<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task Save<TEntity>(TEntity entity) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public Task SaveChanges(bool acceptChanges = true)
		{
			throw new NotImplementedException();
		}

		public Task<User?> SetCrtUser(int? userId)
		{
			throw new NotImplementedException();
		}

		public Task<TEntity?> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public void Update<TEntity>(TEntity entity) where TEntity : class
		{
			throw new NotImplementedException();
		}
	}

}

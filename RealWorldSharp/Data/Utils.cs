using LoremNET;
using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.Data;

public class Utils
{
	static List<User> createUsers()
	{
		return
		[
			new User
			{
				UserId = 1,
				Email = "eric-simons@google.com",
				Username = "eric-simons",
				Password = "abc",
				Image = "https://i.imgur.com/Qr71crq.jpg",
				Bio = LoremNET.Generate.Sentence(5,20),
			},
			new User
			{
				UserId = 2,
				Email = "albert-pai@google.com",
				Username = "albert-pai",
				Password = "abc",
				Image = "https://i.imgur.com/N4VcUeJ.jpg",
				Bio = LoremNET.Generate.Sentence(5,20),
			},
			new User
			{
				UserId = 3,
				Email = "user3@google.com",
				Username = "user3",
				Password = "abc",
				Image = "https://i.stack.imgur.com/xHWG8.jpg",
				Bio = LoremNET.Generate.Sentence(5,20),
			},
			new User
			{
				UserId = 4,
				Email = "user4@google.com",
				Username = "user4",
				Password = "abc",
				Image = "https://i.stack.imgur.com/xHWG8.jpg",
				Bio = LoremNET.Generate.Sentence(5,20),
			},
		];
	}

	static List<Article> createArticles(int articleCount)
	{
		Random rand = new Random();

		List<Article> list = new();
		for (int i = 1; i <= articleCount; i++)
			list.Add(
				new Article
				{
					ArticleId = i,
					Slug = Guid.NewGuid().ToString(),
					Title = LoremNET.Generate.Sentence(5, 20),
					Description = LoremNET.Generate.Sentence(5, 20),
					Body = LoremNET.Generate.Paragraph(10, 50, 1, 10),
					UserId = rand.Next(1, 5),
					CreatedAt = LoremNET.Generate.DateTime(DateTime.Now.AddDays(-100)),
				}
			);

		return list;
	}

	static List<Tag> createTags(int tagCount, int articleCount)
	{
		Random rand = new Random();
		List<Tag> list = new();
		List<string> tags = new ();

		for (int i = 1; i <= tagCount; i++)
			tags.Add(LoremNET.Generate.Words(1));

		for (int i = 1; i <= tags.Count; i++)
			list.Add(
				new Tag
				{
					TagId = i,
					TagName = tags[rand.Next(0, tags.Count)],
					ArticleId = rand.Next(1, articleCount + 1),
				}
			);

		return list;
	}

	static List<ArticleFavorite> createFavorites()
	{
		return
		[
			new ArticleFavorite
			{
				ArticleFavoriteId = 1,
				ArticleId = 1,
				UserId = 2,
			},
			new ArticleFavorite
			{
				ArticleFavoriteId = 2,
				ArticleId = 2,
				UserId = 1,
			},
			new ArticleFavorite
			{
				ArticleFavoriteId = 3,
				ArticleId = 1,
				UserId = 3,
			},
		];
	}

	public static void SeedContext(RealWorldContext context)
	{
		int articleCount = 8;
		int tagCount = 12;

		//context.Database.EnsureCreated();
		//context.Database.EnsureDeleted();
		context.AddRange(createUsers());
		context.AddRange(createArticles(articleCount));
		context.AddRange(createTags(tagCount, articleCount));
		context.AddRange(createFavorites());
		context.SaveChanges();

	}
}

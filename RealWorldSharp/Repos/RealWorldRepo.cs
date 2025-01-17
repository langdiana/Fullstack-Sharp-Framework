using Microsoft.EntityFrameworkCore;

namespace RealWorldSharp.Repos;

public class RealWorldRepo: RepositoryBase, IRealWorldRepo
{
	RealWorldContext context;

	public RealWorldRepo(RealWorldContext context, CancellationToken cancellationToken) : base(context, cancellationToken)
	{
		this.context = context;
	}

	int RowsPerPage = 5;
	int? CrtUserId;
	User? CrtUser;

	public async Task<User?> SetCrtUser(int? userId)
	{
		CrtUserId = userId;
		if (userId == null)
			CrtUser = null;
		else 
			CrtUser = await GetUserById(userId.Value);

		return CrtUser;
	}

	public UserModel? GetCrtUserModel()
	{
		if (CrtUser == null) 
			return null;
		return new UserModel().FromEntity(CrtUser);
	}

	public async Task<User?> GetUserById(int userId)
	{
		var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId, CancellationToken);
		return user;
	}

	public async Task DeleteArticle(int articleId)
	{
		var article = await context.Articles.Where(x => x.ArticleId == articleId)
			.Include(x => x.Tags)
			.Include(x => x.ArticleFavorites)
			.Include(x => x.Comments)
			.FirstOrDefaultAsync();

		if (article != null)
		{
			context.Remove(article);
			await context.SaveChangesAsync(CancellationToken);
		}

	}

	IQueryable<ArticleModel> getArticleModelsLinq()
	{
		var linq = context.Articles
			.Include(x => x.Author)
			.Include(x => x.Tags)
			.Include(x => x.ArticleFavorites)
			.Include(x => x.Comments).ThenInclude(x => x.Author)
			.OrderBy(x => x.ArticleId)
			.Select(x => new ArticleModel
			{
				ArticleId = x.ArticleId,
				Title = x.Title,
				Description = x.Description,
				Slug = x.Slug,
				CreatedAt = x.CreatedAt,
				UpdatedAt = x.UpdatedAt,
				Body = x.Body,
				FavoritesCount = x.ArticleFavorites.Count,
				Tags = x.Tags.Select(x => x.TagName).ToList(),
				ArticleFavorites = x.ArticleFavorites,
				Comments = x.Comments,
				Author = new UserModel
				{
					UserId = x.Author.UserId,
					Username = x.Author.Username,
					Image = x.Author.Image,
				},
			}
			
		);

		return linq;
	}

	public async Task<List<ArticleModel>> GetArticlesByAuthor(int userId)
	{
		var linq = getArticleModelsLinq();
		return await linq.Where(x => x.Author.UserId == userId).ToListAsync(CancellationToken);
	}

	async Task<List<User>> getFollowingUser(int userId, int targetUserId)
	{
		var linq =
			from followUser in context.UserFollows where followUser.TargetUserId == targetUserId && followUser.SourceUserId == userId
			from user in context.Users where user.UserId == followUser.SourceUserId
			select user;

		return await linq.ToListAsync(CancellationToken);
	}

	IQueryable<User> getFollowerUsersLinq(int targetUserId)
	{
		var linq =
			from followUser in context.UserFollows where followUser.TargetUserId == targetUserId
			from user in context.Users where user.UserId == followUser.SourceUserId
			select user;

		return linq;
	}

	async Task<List<User>> getFollowerUsers(int targetUserId)
	{
		var linq = getFollowerUsersLinq(targetUserId);
		return await linq.ToListAsync(CancellationToken);
	}

	async Task<int> getFollowerUsersCount(int targetUserId)
	{
		var linq = getFollowerUsersLinq(targetUserId);
		return await linq.CountAsync(CancellationToken);
	}

	async Task<bool> isFollowingUser(int userId, int targetUserId)
	{
		return await context.UserFollows.AnyAsync(x => x.TargetUserId == targetUserId && x.SourceUserId == userId, CancellationToken);
	}

	async Task<ArticleModel?> getArticle(IQueryable<ArticleModel> linq)
	{
		var article = await linq.FirstOrDefaultAsync(CancellationToken);
		if (article != null)
		{
			article.Author.FollowerUsers = await getFollowerUsers(article.Author.UserId);
			if (CrtUserId != null)
			{
				article.Author.Following = await isFollowingUser(CrtUserId.Value, article.Author.UserId);
				article.CrtUser = CrtUser;
			}
		}
		return article;
	}

	public async Task<ArticleModel?> GetArticleBySlug(string slug)
	{
		var linq = getArticleModelsLinq().Where(x => x.Slug == slug);
		var article = await getArticle(linq);

		return article;
	}

	public async Task<ArticleModel?> GetArticleById(int articleId)
	{
		var linq = getArticleModelsLinq().Where(x => x.ArticleId == articleId);
		var article = await getArticle(linq);

		return article;
	}

	public async Task<List<ArticleModel>> GetArticles(int pageNumber)
	{
		var linq = getArticleModelsLinq().Skip(pageNumber - 1).Take(RowsPerPage);

		var articles = await linq.ToListAsync(CancellationToken);

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

	async Task<List<ArticleModel>> getArticles(IQueryable<ArticleModel> linq, int pageNumber)
	{
		var articles = await linq.Skip((pageNumber - 1) * RowsPerPage).Take(RowsPerPage).ToListAsync(CancellationToken);

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

	public async Task GetArticlesForProfile(GetArticlesForProfileCommand cmdRepo)
	{
		var linq = getArticleModelsLinq();
		if (cmdRepo.PagerInfo.IsMyArticles)
			linq = linq.Where(x => x.Author.UserId == cmdRepo.PagerInfo.ProfileUserId);
		else
			linq = linq.Where(x => x.ArticleFavorites.Any(x => x.UserId == cmdRepo.PagerInfo.ProfileUserId));
		
		cmdRepo.PagerInfo.ItemCount = await linq.CountAsync(CancellationToken);
		cmdRepo.PagerInfo.RowsPerPage = RowsPerPage;
		cmdRepo.Articles = await getArticles(linq, cmdRepo.PagerInfo.PageNumber);
	}

	public async Task<ProfileModel?> GetProfileModel(string username, bool isMyArticles)
	{
		ProfileModel? profile = await context.Users
			.Select(x => new ProfileModel
			{
				UserId = x.UserId,
				Username = x.Username,
				Bio = x.Bio,
				Image = x.Image,
			})
			.FirstOrDefaultAsync(x => x.Username == username);
		
		if (profile == null)
			return null;

		if (CrtUser != null)
			profile.CrtUser = CrtUser;

		GetArticlesForProfileCommand cmd = new();
		cmd.PagerInfo.IsMyArticles = isMyArticles;
		cmd.PagerInfo.ProfileUserId = profile.UserId;
		cmd.PagerInfo.RowsPerPage = RowsPerPage;
		cmd.PagerInfo.PageNumber = 1;
		await GetArticlesForProfile(cmd);

		profile.Articles = cmd.Articles;
		profile.PagerInfo = cmd.PagerInfo;

		profile.FollowerCount = await getFollowerUsersCount(profile.UserId);
		profile.Following = CrtUser != null ? await isFollowingUser(CrtUser.UserId, profile.UserId) : false;

		return profile;
	}

	public async Task<ProfileModel?> GetProfileModelShort(string username)
	{
		ProfileModel? profile = await context.Users
			.Select(x => new ProfileModel
			{
				UserId = x.UserId,
				Username = x.Username,
				Bio = x.Bio,
				Image = x.Image,
			})
			.FirstOrDefaultAsync(x => x.Username == username);

		if (profile == null)
			return null;

		profile.FollowerCount = await getFollowerUsersCount(profile.UserId);
		profile.Following = CrtUser != null ? await isFollowingUser(CrtUser.UserId, profile.UserId) : false;

		return profile;
	}

	public async Task GetArticlesForHome(GetArticlesForHomeCommand cmdRepo)
	{
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

		cmdRepo.PagerInfo.ItemCount = await linq.CountAsync(CancellationToken);
		cmdRepo.PagerInfo.RowsPerPage = RowsPerPage;
		cmdRepo.Articles = await getArticles(linq, cmdRepo.PagerInfo.PageNumber);
	}

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

		hm.Tags = await context.Tags.Take(10).ToListAsync(CancellationToken); // TODO: order by name count

		return hm;
	}

}

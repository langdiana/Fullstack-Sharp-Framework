using RealWorldSharp.Base;
using RealWorldSharp.Data.Models;

namespace RealWorldSharp.Interfaces;

public interface IRealWorldRepo: IRepositoryBase
{
	Task<User?> SetCrtUser(int? userId);
	UserModel? GetCrtUserModel();
	Task<List<ArticleModel>> GetArticles(int pageNumber);
	Task<List<ArticleModel>> GetArticlesByAuthor(int userId);
	Task<ArticleModel?> GetArticleBySlug(string slug);
	Task<ArticleModel?> GetArticleById(int articleId);
	Task<ProfileModel?> GetProfileModel(string username, bool isMyArticles);
	Task<ProfileModel?> GetProfileModelShort(string username);
	Task<HomeModel> GetHomeModel(FeedTypeEnum feedType, string? tag = null);
	Task<User?> GetUserById(int userId);
	Task DeleteArticle(int articleId);

	Task GetArticlesForProfile(GetArticlesForProfileCommand cmd);
	Task GetArticlesForHome(GetArticlesForHomeCommand cmd);

}

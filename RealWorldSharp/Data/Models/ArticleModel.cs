using System.Web;

namespace RealWorldSharp.Data.Models;

public class ArticleModel
{
	public int ArticleId { get; set; }

	public string Slug { get; set; } = null!;

	public string Title { get; set; } = null!;

	public string Description { get; set; } = null!;

	public string Body { get; set; } = null!;

	public string HtmlBody => HttpUtility.HtmlEncode(Body);

	public UserModel Author { get; set; } = null!;

	public User? CrtUser { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public bool Favorited { get; set; }

	public int FavoritesCount { get; set; } = 0;

	public List<string> Tags { get; set; } = new ();

	public List<ArticleFavorite> ArticleFavorites { get; set; } = new();

	public List<Comment> Comments { get; set; } = new();

	public string GetFollowJson()
	{
		FollowUserModel follow = new() { TargetUserId = Author.UserId, Slug = Slug ?? "" };
		return JsonConvert.SerializeObject(follow);
	}

	public string GetFavoriteJson()
	{
		ArticleFavoriteModel fav = new() { ArticleId = ArticleId };
		return JsonConvert.SerializeObject(fav);
	}

	public bool IsAuthor => Author == null ? true : CrtUser?.UserId == Author.UserId;

	public Article ToEntity()
	{
		List<Tag> ToTagList()
		{
			List<Tag> tagList = new();
			foreach (var tag in Tags)
				tagList.Add(new() { TagName = tag });

			return tagList;
		}

		return new Article
		{
			Slug = Slug,
			Title = Title,
			Description = Description,
			Body = Body,
			Tags = ToTagList(),
		};

	}


}

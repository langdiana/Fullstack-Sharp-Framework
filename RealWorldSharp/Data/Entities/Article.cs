namespace RealWorldSharp.Data.Entities;

public class Article
{
	[Key]
	public int ArticleId { get; set; }

	[MaxLength (100)]
	public string Slug { get; set; } = "";

	[MaxLength(100)]
	public string Title { get; set; } = "";

	[MaxLength(100)]
	public string Description { get; set; } = "";

	[MaxLength(5000)]
	public string Body { get; set; } = "";

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public int UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public User Author { get; set; } = null!;

	public List<Tag> Tags { get; set; } = new();

	public List<Comment> Comments { get; set; } = new();
	public List<ArticleFavorite> ArticleFavorites { get; set; } = new();
}

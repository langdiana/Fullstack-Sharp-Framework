namespace RealWorldSharp.Data.Entities;

public class ArticleFavorite
{
	[Key]
	public int ArticleFavoriteId { get; set; }
	public int ArticleId { get; set; }
	public int UserId { get; set; }
}

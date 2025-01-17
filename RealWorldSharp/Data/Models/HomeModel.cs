namespace RealWorldSharp.Data.Models;

public class HomeModel
{
	public User? CrtUser { get; set; }
	public List<ArticleModel> Articles { get; set; } = new();
	public List<Tag> Tags { get; set; } = new();

	public HomePagerInfo PagerInfo { get; set; } = new();
}

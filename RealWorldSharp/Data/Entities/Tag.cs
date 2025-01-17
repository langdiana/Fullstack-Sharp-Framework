namespace RealWorldSharp.Data.Entities;

public class Tag
{
	[Key]
	public int TagId { get; set; }
	public string TagName { get; set; } = null!;
	public int ArticleId { get; set; }

	[ForeignKey(nameof(ArticleId))]
	public Article Article { get; set; } = null!;
}

namespace RealWorldSharp.Data.Entities;

public class Comment
{
	[Key]
	public int CommentId { get; set; }
	public int ArticleId { get; set; }
	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public string Body { get; set; } = "";
	public int UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public User Author { get; set; } = null!;
}

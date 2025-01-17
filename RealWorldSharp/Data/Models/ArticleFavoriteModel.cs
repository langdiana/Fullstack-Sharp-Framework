namespace RealWorldSharp.Data.Models;

public class ArticleFavoriteModel
{
	public int ArticleId { get; set; }
}

public class HomePagerModel
{
	public int PageNumber { get; set; }
	public FeedTypeEnum FeedType { get; set; }
	public string? Tag { get; set; }
}

public class ProfilePagerModel
{
	public bool IsMyArticles { get; set; }
	public int PageNumber { get; set; }
	public int ProfileUserId { get; set; }
}

public class FollowUserModel
{
	public string Slug { get; set; } = "";
	public int TargetUserId { get; set; }
}

public class FollowUserProfileModel
{
	public string Username { get; set; } = "";
	public int TargetUserId { get; set; }
}

public class PostCommentModel
{
	public int ArticleId { get; set; }
	public string Body { get; set; } = "";
}

public class DeleteCommentModel
{
	public int CommentId { get; set; }
	public int ArticleId { get; set; }
}

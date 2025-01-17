namespace RealWorldSharp.Data.Entities;

public class UserFollow
{
	[Key]
	public int UserFollowId { get; set; }
	public int SourceUserId { get; set; }
	public int TargetUserId { get; set; }
}

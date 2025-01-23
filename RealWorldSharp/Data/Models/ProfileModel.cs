namespace RealWorldSharp.Data.Models;

public class ProfileModel
{
	public int UserId { get; set; }
	public string Username { get; set; } = "";

	public string? Bio { get; set; }

	public string? Image { get; set; }

	public bool Following { get; set; }
	public User? CrtUser { get; set; }
	public List<ArticleModel> Articles { get; set; } = new();
	public bool IsCrtUser => CrtUser?.UserId == UserId;
	public int FollowerCount { get; set; }

	public string FollowJson => JsonConvert.SerializeObject(new FollowUserProfileModel {Username = Username,  TargetUserId = UserId });

	public ProfilePagerInfo PagerInfo { get; set; } = new();

}

namespace RealWorldSharp.Data.Entities;

public class User
{
	[Key]
	public int UserId { get; set; }

	public string Username { get; set; } = "";

	public string Email { get; set; } = "";

	public string Password { get; set; } = "";

	public string Bio { get; set; } = string.Empty;

	public string Image { get; set; } = string.Empty;

}

namespace RealWorldSharp.Data.Entities;

public class User
{
	[Key]
	public int UserId { get; set; }

	public string Username { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string? Bio { get; set; }

	public string? Image { get; set; }

}

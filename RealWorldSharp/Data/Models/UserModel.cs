using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RealWorldSharp.Data.Models;

public class UserModel
{
	public int UserId { get; set; }

	public string Username { get; set; } = default!;

	public string Email { get; set; } = default!;

	public string Password { get; set; } = default!;

	public string Bio { get; set; } = string.Empty;

	public string Image { get; set; } = string.Empty;

	public bool Following { get; set; }

	public List<User> FollowerUsers { get; set; } = new();

	public User ToEntity()
	{
		return new()
		{
			UserId = UserId,
			Username = Username,
			Email = Email,
			Password = Password,
			Image = Image,
			Bio = Bio,
		};
	}

	public UserModel FromEntity(User user)
	{
		return new()
		{
			UserId = user.UserId,
			Username = user.Username,
			Email = user.Email,
			Password = user.Password,
			Image = user.Image,
			Bio = user.Bio,
		};
	}

}

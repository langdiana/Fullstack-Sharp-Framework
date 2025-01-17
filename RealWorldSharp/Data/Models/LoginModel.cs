using System.ComponentModel.DataAnnotations;

namespace RealWorldSharp.Data.Models;

public class LoginModel
{
	public string Email { get; set; } = null!;

	public string Password { get; set; } = null!;
}

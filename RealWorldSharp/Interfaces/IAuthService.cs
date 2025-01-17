using RealWorldSharp.Data.Entities;

namespace RealWorldSharp.Interfaces;

public interface IAuthService
{
	Task SignIn(User user);
	Task SignOut();
}

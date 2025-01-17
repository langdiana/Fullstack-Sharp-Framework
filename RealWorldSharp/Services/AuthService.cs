using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RealWorldSharp.Data.Entities;
using RealWorldSharp.Data.Models;
using RealWorldSharp.Interfaces;
using System.Security.Claims;

namespace RealWorldSharp.Services;

public class AuthService: IAuthService
{
	public AuthService(HttpContext httpContext)
	{
		this.httpContext = httpContext;
	}

	HttpContext httpContext;

	public async Task SignIn(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
		};

		var scheme = CookieAuthenticationDefaults.AuthenticationScheme;
		var claimsIdentity = new ClaimsIdentity(claims, scheme);
		var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
		var authProperties = new AuthenticationProperties();
		authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
		authProperties.IsPersistent = true;

		await httpContext.SignInAsync(scheme, claimsPrincipal, authProperties);
	}

	public async Task SignOut()
	{
		await httpContext.SignOutAsync();
	}

}

public static class Auth
{

	public static void AddAuthFeature(this WebApplicationBuilder builder)
	{
		var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

		builder.Services.AddAuthentication(scheme)
			.AddCookie(scheme, options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			});

		builder.Services.AddAuthorization();
	}

	public static IApplicationBuilder UseAuthFeature(this IApplicationBuilder app)
	{
		return app
			.UseAuthentication()
			.UseAuthorization();
	}
}

public static class Cors
{
	public static void AddCorsFeature(this WebApplicationBuilder builder)
	{
		builder.Services
			.AddCors(policy => policy
			.AddDefaultPolicy(builder => builder
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod()));
	}

	public static IApplicationBuilder UseCorsFeature(this IApplicationBuilder app)
	{
		return app.UseCors();
	}
}

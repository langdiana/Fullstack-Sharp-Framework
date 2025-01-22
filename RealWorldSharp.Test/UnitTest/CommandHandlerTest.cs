using RealWorldSharp.CommandHandlers;
using RealWorldSharp.Commands;
using RealWorldSharp.Data;
using RealWorldSharp.Data.Entities;
using RealWorldSharp.Interfaces;
using RealWorldSharp.UI;
using static RealWorldSharp.Test.TestUtils;

namespace RealWorldSharp.Test.UnitTest;

[TestClass]
public class CommandHandlerTest
{
	RealWorldContext context = null!;
	public IUIBuilder uiBuilder = null!;
	public IAuthService authService = null!;

	public CommandHandlerTest()
	{
		uiBuilder = new UIBuilder(true, "test");
		authService = new FakeAuthService();
	}

	[TestMethod]
	public async Task GetHomePageAuthenticated()
	{
		User crtUser = new() { UserId = 1, Username = "CrtUser", Email = "email" };
		IRealWorldRepo repo = new FakeRepo(crtUser);
		var cmd = new GetHomePageCommand { };
		GetHomePageHandler handler = new() { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
		await handler.Execute(cmd);
		Assert.IsTrue(cmd.OK, cmd.ErrorMessage);

		var html = ((Microsoft.AspNetCore.Http.HttpResults.ContentHttpResult)cmd.Result).ResponseContent;
		Assert.IsNotNull(html);

		HtmlTest hTest = new(html);
		var rootClass = hTest.GetRootClass();
		Assert.IsTrue(rootClass == "home-page");
		Assert.IsTrue(hTest.HasText("a", "Your Feed"));
		Assert.IsTrue(hTest.HasText("a", "Global Feed"));
	}

	[TestMethod]
	public async Task GetHomePageNotAuthenticated()
	{
		IRealWorldRepo repo = new FakeRepo(null);
		var cmd = new GetHomePageCommand { };
		GetHomePageHandler handler = new() { Repo = repo, UiBuilder = uiBuilder, AuthService = authService };
		await handler.Execute(cmd);
		Assert.IsTrue(cmd.OK, cmd.ErrorMessage);

		var html = ((Microsoft.AspNetCore.Http.HttpResults.ContentHttpResult)cmd.Result).ResponseContent;
		Assert.IsNotNull(html);

		HtmlTest hTest = new(html);
		var rootClass = hTest.GetRootClass();
		Assert.IsTrue(rootClass == "home-page");
		Assert.IsFalse(hTest.HasText("a", "Your Feed"));
		Assert.IsTrue(hTest.HasText("a", "Global Feed"));
	}

}

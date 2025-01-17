namespace RealWorldSharp.CommandHandlers;


public class GetHomePageHandler: CommandHandlerBase<GetHomePageCommand>
{

	public override async Task Execute(GetHomePageCommand cmd)
	{
		var home = await GetHomePage(FeedTypeEnum.Global);
		cmd.Result = UiBuilder.RenderPage(home);
	}
}

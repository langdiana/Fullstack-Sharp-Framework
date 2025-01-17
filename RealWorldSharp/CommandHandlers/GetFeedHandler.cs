namespace RealWorldSharp.CommandHandlers;

public class GetFeedHandler : CommandHandlerBase<GetFeedCommand>
{

	public override async Task Execute(GetFeedCommand cmd)
	{
		var home = await GetHomePage(cmd.FeedType, cmd.Tag);
		cmd.Result = UiBuilder.RenderPage(home);
	}
}

namespace RealWorldSharp.CommandHandlers;

public class PostCommentHandler : CommandHandlerBase<PostCommentCommand>
{
	public override async Task Execute(PostCommentCommand cmd)
	{

		Comment comment = new()
		{
			ArticleId = cmd.Comment.ArticleId,
			UserId = cmd.UserId.GetValueOrDefault(),
			Body = cmd.Comment.Body,
			CreatedAt = DateTime.UtcNow,
		};

		await Repo.Save(comment);

		var page = await GetArticlePage(cmd.Comment.ArticleId);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

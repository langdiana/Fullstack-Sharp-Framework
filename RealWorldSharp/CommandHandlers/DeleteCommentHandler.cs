namespace RealWorldSharp.CommandHandlers;

public class DeleteCommentHandler : CommandHandlerBase<DeleteCommentCommand>
{
	public override async Task Execute(DeleteCommentCommand cmd)
	{

		Comment comment = new()
		{
			CommentId = cmd.Comment.CommentId,
		};

		Repo.Remove(comment);
		await Repo.SaveChanges();

		var page = await GetArticlePage(cmd.Comment.ArticleId);
		cmd.Result = UiBuilder.RenderPage(page);
	}
}

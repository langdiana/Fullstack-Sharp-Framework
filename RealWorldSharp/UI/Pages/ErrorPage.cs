namespace RealWorldSharp.UI.Pages
{
	public class ErrorPage
	{
	}

	public static partial class Pages
	{
		public static HtmlElement ErrorPage(string errorMessage, string? errorDetail)
		{
			return
			div(new() { className = "error-messages" },
				h1(_, errorMessage
				),
				p(_, errorDetail ?? ""
				)
			);

		}


	}

}

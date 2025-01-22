namespace RealWorldSharp.Data.Models;

public abstract class PagerInfo
{
	public int ItemCount { get; set; }
	public int RowsPerPage { get; set; }
	public int PageNumber { get; set; }
	public abstract string GetJson(int pageNumber);
	public abstract string GetRoute();
}

public class HomePagerInfo: PagerInfo
{
	public FeedTypeEnum FeedType { get; set; }
	public string? Tag { get; set; }

	public override string GetJson(int pageNumber)
	{
		HomePagerModel counter = new() { PageNumber = pageNumber, FeedType = FeedType, Tag = Tag };
		return JsonConvert.SerializeObject(counter);
	}

	public override string GetRoute()
	{
		return Routes.HomePager;
	}
}

public class ProfilePagerInfo : PagerInfo
{
	public bool IsMyArticles { get; set; }
	public int ProfileUserId { get; set; }

	public override string GetJson(int pageNumber)
	{
		ProfilePagerModel counter = new() { PageNumber = pageNumber, IsMyArticles = IsMyArticles, ProfileUserId = ProfileUserId };
		return JsonConvert.SerializeObject(counter);
	}

	public override string GetRoute()
	{
		return Routes.ProfilePager;
	}
}

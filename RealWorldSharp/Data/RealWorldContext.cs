using Microsoft.EntityFrameworkCore;

namespace RealWorldSharp.Data;

public class RealWorldContext: DbContext
{
	public RealWorldContext(DbContextOptions<RealWorldContext> options) : base(options)
	{
	}

	public virtual DbSet<Article> Articles { get; set; }
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Tag> Tags { get; set; }
	public virtual DbSet<ArticleFavorite> ArticleFavorite { get; set; }
	public virtual DbSet<UserFollow> UserFollows { get; set; }
}

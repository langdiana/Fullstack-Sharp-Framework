using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RealWorldSharp.Base;

public class RepositoryBase : IRepositoryBase
{

	DbContext context = null!;
	protected CancellationToken CancellationToken;

	public RepositoryBase(DbContext context, CancellationToken cancellationToken)
	{
		this.context = context;
		this.CancellationToken = cancellationToken;
	}

	public async Task SaveChanges(bool acceptChanges = true)
	{
		await context.SaveChangesAsync(acceptChanges);
	}

	public void ClearChangeTracker()
	{
		context.ChangeTracker.Clear();
	}

	public void Add<TEntity>(TEntity entity) where TEntity : class
	{
		context.Update(entity);
	}

	public void Update<T>(T entity) where T : class
	{
		context.Update(entity);
	}

	public void Remove<TEntity>(TEntity entity) where TEntity : class
	{
		context.Remove(entity);
	}

	public void RemoveRange<TEntity>(List<TEntity> entityList) where TEntity : class
	{
		context.RemoveRange(entityList);
	}

	public void RemoveRange<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
	{
		context.RemoveRange(context.Set<TEntity>().Where(condition));
	}

	public async Task Save<TEntity>(TEntity entity) where TEntity : class
	{
		context.Update(entity);
		await context.SaveChangesAsync();
	}

	public async Task<TEntity?> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
	{
		return await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(condition);
	}

	public async Task<TEntity?> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
	{
		return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(condition);
	}

	public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : class
	{
		return await context.Set<TEntity>().ToListAsync();
	}

	public async Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
	{
		return await context.Set<TEntity>().Where(condition).ToListAsync();
	}

	public async Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
	{
		return await context.Set<TEntity>().AnyAsync(condition);
	}

}

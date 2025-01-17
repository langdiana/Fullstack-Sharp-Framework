using System.Linq.Expressions;

namespace RealWorldSharp.Base;

public interface IRepositoryBase
{
    Task SaveChanges(bool acceptChanges = true);

    void ClearChangeTracker();

    void Add<TEntity>(TEntity entity) where TEntity : class;

    void Update<TEntity>(TEntity entity) where TEntity : class;

    void Remove<TEntity>(TEntity entity) where TEntity : class;

    void RemoveRange<TEntity>(List<TEntity> entityList) where TEntity : class;

    void RemoveRange<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

    Task Save<TEntity>(TEntity entity) where TEntity : class;

    Task<TEntity?> SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

    Task<TEntity?> FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

    Task<List<TEntity>> GetAll<TEntity>() where TEntity : class;

    Task<List<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;

    Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
}
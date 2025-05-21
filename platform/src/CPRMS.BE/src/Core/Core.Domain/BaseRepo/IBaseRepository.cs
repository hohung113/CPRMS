//using System.Linq.Expressions;

//namespace Core.Domain.BaseRepo
//{
//    public interface IBaseRepository<TEntity> : IBaseQuery<TEntity> where TEntity : IEntity
//    {
//        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

//        Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

//        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

//        Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

//        Task SoftDeleteAsync(Guid key, CancellationToken cancellationToken = default);

//        Task SoftDeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

//        Task SoftDeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

//        Task DeleteAsync(Guid key, CancellationToken cancellationToken = default);

//        Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

//        Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
//    }
//}

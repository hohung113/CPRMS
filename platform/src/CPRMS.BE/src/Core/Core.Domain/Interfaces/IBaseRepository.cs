using Core.Domain.Models.Base;
using Core.Domain.Query;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Core.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetEntities();
        public Task<TEntity?> GetEntity(Guid id);
        public Task<List<TEntity>> UpdateEntities(List<TEntity> entities);
        public Task<Boolean> UpdateEntity(TEntity entity, bool isUpdateModifiedOn = true);
        public Task<TEntity> UpdateEntity(TEntity entity, Tuple<String, SqlParameter[]> condition);
        public Task<TEntity> AddEntity(TEntity entity);
        public Task<List<TEntity>> AddEntities(List<TEntity> entities);
        public Task<Boolean> DeleteEntity(TEntity entity);
        public Task<Boolean> DeleteEntities(List<TEntity> entities);
        public Task<BaseDataCollection<TEntity>> QueryEntities(PageModel page);
        public Task<List<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate, bool findIsDelete = false);
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}

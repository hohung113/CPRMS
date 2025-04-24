using Core.Repository.Models.Base;
using Core.Repository.Query;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Interfaces
{
    public interface IBaseRepository<TContext, TEntity>
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
    }
}

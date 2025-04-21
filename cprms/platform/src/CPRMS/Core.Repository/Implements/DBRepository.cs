using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.Implements
{
    public class DBRepository : IDBRepository
    {
        public DbContext Context => throw new NotImplementedException();

        public Task<T> AddAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> AddValue<TEntity>(TEntity entity, Guid? userId = null) where TEntity : BaseEntity
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Guid autherID = new Guid();
            entity.Id  = Guid.NewGuid();
            entity.CreateDate = now;
            entity.UpdateDate = now;
            entity.CreateBy = autherID;
            entity.UpdateBy = autherID;
            entity.IsDelete = false;
            Context.Set<TEntity>().Add(entity);
            return await Task.FromResult(entity);
        }

        public void BulkInsertOrUpdate<T>(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public int DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Execute(Action<DbContext> expression)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] param)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(string formattedSql)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TResult> Filter<T, TResult>(Expression<Func<T, bool>> expression = null, Expression<Func<T, TResult>> selector = null) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TResult, bool>> expression, bool track = true) where TResult : class
        {
            throw new NotImplementedException();
        }

        public Task<TResult> FirstOrDefaultAsync<T, TResult>(Expression<Func<T, bool>> expression = null, Expression<Func<T, TResult>> selector = null) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FromSql<T>(string sql, params object[] param) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FromSql<T>(string formattedSql) where T : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TResult> Query<TResult>(Func<DbContext, IQueryable<TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> SqlQueryAsync<T>(string sql, params object[] param)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> updateFactory) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateFieldsAsync<T>(T entity, List<string> fields) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateFieldsRangeAsync<T>(IList<T> entities, List<string> fields) where T : class
        {
            throw new NotImplementedException();
        }
    }
}

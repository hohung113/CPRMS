using Core.Domain.Context;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Core.Domain.Models.Base;
using Core.Domain.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rms.Domain.Context;
using System.Linq.Expressions;

namespace Core.Infrastructure.Repositories
{
    public class BaseRepository<TContext, TEntity> : IDisposable, IBaseRepository<TEntity>
        where TContext : DbContext
        where TEntity : BaseEntity
    {
        protected TContext _context;
        protected Func<TContext>? _func;
        private Guid CurrentUserId
        {
            get
            {
                var userId = CPRMSHttpContext.UserId;
                if (Guid.TryParse(userId, out var guid))
                {
                    return guid;
                }
                throw new InvalidOperationException("Current UserID is invalid or missing.");
            }
        }

        private DateTimeOffset CurrentTime
        {
            get
            {
                return DateTimeOffset.UtcNow;
            }
        }
        private readonly ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "Entity");

        public BaseRepository(Func<TContext> func)
        {
            this._context = func.Invoke();
            this._func = func;
        }
        public BaseRepository(TContext dbContext)
            {
            _context = dbContext;
        }

        public async Task<List<TEntity>> AddEntities(List<TEntity> entities)
        {
            var preparedEntities = AddEntitiesCommon(entities);

            await _context.Set<TEntity>().AddRangeAsync(preparedEntities);
            await _context.SaveChangesAsync();

            return preparedEntities;
        }

        public async Task<TEntity> AddEntity(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await this.AddEntityCommon(entity);
        }

        public async Task<bool> DeleteEntities(List<TEntity> entities)
        {
            var entitiesToDelete = DeleteEntitiesCommon(entities);
            _context.Set<TEntity>().UpdateRange(entitiesToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEntity(TEntity entity)
        {
            var entityToDelete = DeleteEntityCommon(entity);
            _context.Set<TEntity>().Update(entityToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<List<TEntity>> GetEntities()
        {
            return await _context.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();
        }

        public virtual async Task<TEntity?> GetEntity(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }


        public virtual async Task<BaseDataCollection<TEntity>> QueryEntities(PageModel page)
        {
            if (page.Total <= 0)
            {
                page.Total = await _context.Set<TEntity>().CountAsync(e => !e.IsDeleted);
            }
            var query = _context.Set<TEntity>()
                                .Where(e => !e.IsDeleted)
                                .OrderBy(e => e.Id); 

            var entities = await query.Skip(page.Start)
                                       .Take(page.PageSize)
                                       .ToListAsync();


            var pageCount = (int)Math.Ceiling((decimal)page.Total / page.PageSize);

            return new BaseDataCollection<TEntity>
            {
                BaseDatas = entities,
                TotalRecordCount = (int)page.Total,
                HasPermission = true,
                PageIndex = page.PageNo, 
                PageCount = pageCount 
            };
        }


        public virtual async Task<List<TEntity>> UpdateEntities(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                throw new ArgumentException("Entities list cannot be null or empty.", nameof(entities));
            }
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity), "One of the entities is null.");
                }
                var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);

                if (existingEntity == null)
                {
                    throw new InvalidOperationException($"Entity with ID {entity.Id} not found.");
                }
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.LastModified = this.CurrentTime;
                existingEntity.LastModifiedBy = this.CurrentUserId;
            }
            _context.Set<TEntity>().UpdateRange(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public virtual async Task<Boolean> UpdateEntity(TEntity entity, bool isUpdateModifiedOn = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await UpdateEntityCommon(entity, isUpdateModifiedOn);
        }

        public virtual async Task<TEntity> UpdateEntity(TEntity entity, Tuple<string, SqlParameter[]> condition)
        {
            // Nen thiet ke sao cho can chuyen doi giua EF Core va sql raw cho mot so truong hop can thiet
            throw new Exception();
        }


        /// <summary>
        ///  
        /// </summary>
        /// <typeparam name="ConfigModel"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual async Task<TEntity> AddEntityCommon(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            var now = DateTimeOffset.UtcNow;

            // Audit information
            entity.CreatedAt = now;
            entity.LastModified = now;
            entity.LastModifiedBy = this.CurrentUserId;
            entity.IsDeleted = false;
            ValidateEntity(entity);

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        private List<TEntity> AddEntitiesCommon(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
                throw new ArgumentException("The entity list cannot be null or empty.", nameof(entities));

            var now = DateTimeOffset.UtcNow;

            foreach (var entity in entities)
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity in the list cannot be null.");

                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedAt = now;
                entity.LastModified = now;
                entity.LastModifiedBy = this.CurrentUserId;
                entity.IsDeleted = false;

                ValidateEntity(entity); 
            }

            return entities;
        }
        private TEntity DeleteEntityCommon(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            if (entity.IsDeleted)
                throw new InvalidOperationException("The entity has already been marked as deleted.");
            // Set entity's deletion information
            entity.LastModified = this.CurrentTime;
            entity.LastModifiedBy = this.CurrentUserId;
            entity.IsDeleted = true; // Soft delete

            return entity;
        }

        private List<TEntity> DeleteEntitiesCommon(List<TEntity> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities), "Entities cannot be null or empty.");

            foreach (var entity in entities)
            {
                if (entity.IsDeleted)
                    throw new InvalidOperationException($"Entity with ID {entity.Id} has already been marked as deleted.");

                // Mark each entity as deleted
                entity.LastModified = this.CurrentTime;
                entity.LastModifiedBy = this.CurrentUserId;
                entity.IsDeleted = true;
            }

            return entities;
        }

        private async Task<bool> UpdateEntityCommon(TEntity entity, bool isUpdateModifiedOn = true)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            if (isUpdateModifiedOn)
            {
                existingEntity.LastModified = this.CurrentTime;
                existingEntity.LastModifiedBy = this.CurrentUserId;
            }
            _context.Entry(existingEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        protected virtual void ValidateEntity(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                throw new InvalidOperationException("Entity ID must not be empty.");
            }
        }

        public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
            var notDeleted = Expression.Equal(isDeletedProperty, Expression.Constant(false));
            var body = Expression.AndAlso(notDeleted, Expression.Invoke(predicate, parameter));
            var finalPredicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

            return await _context.Set<TEntity>().Where(finalPredicate).ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return await _context.Set<TEntity>().Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate);
        }
    }
}

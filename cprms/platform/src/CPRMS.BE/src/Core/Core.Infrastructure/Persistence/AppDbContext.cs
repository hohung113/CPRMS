using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Core.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedAt = DateTimeOffset.Now;
                        entry.Entity.LastModified = DateTimeOffset.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTimeOffset.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.LastModified = DateTimeOffset.Now;
                        //entry.Entity.LastModifiedBy =
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

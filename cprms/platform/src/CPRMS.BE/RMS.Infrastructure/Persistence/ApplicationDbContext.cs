using MediatR;
using Microsoft.EntityFrameworkCore;
using RMS.SharedKernel.Domain;
using RMS.SharedKernel.Interfaces;
using System.Reflection;

namespace RMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator? _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator? mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            await DispatchDomainEventsAsync(cancellationToken);
            return result;
        }
        //async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    return await SaveChangesAsync(cancellationToken);
        //}
        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            if (_mediator == null) return;

            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<Guid>>() 
               .Select(e => e.Entity)
               .Where(e => e.DomainEvents.Any())
               .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }
            }
        }
    }
}

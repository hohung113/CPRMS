using Microsoft.EntityFrameworkCore;
using Rms.Domain.Entities;
using Rms.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Infrastructure.Seeders
{
    public class RmsSeeder(RmsDbContext dbContext) : IRmsSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                // Defind another entity
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            List<Role> roles =
                [
                   new Role{
                       
                   }
                ];

            return roles;
        }

    }
}

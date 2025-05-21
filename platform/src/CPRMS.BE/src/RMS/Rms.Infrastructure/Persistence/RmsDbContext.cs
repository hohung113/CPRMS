using Core.Domain.Models.Enums;
using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Rms.Domain.Entities;
using System.Reflection;

namespace Rms.Infrastructure.Persistence
{
    public class RmsDbContext : AppDbContext
    {
        public RmsDbContext(DbContextOptions<RmsDbContext> options) : base(options)
        {
        }
        public DbSet<UserSystem> UserSystems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Speciality> Specialities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var academicAffairsOffice = new Guid("d1f3e47a-6c90-4d12-8f4e-bb10c91d2e0a");
            var studentRole = new Guid("e72b1fc9-89c3-4d14-b1f5-3a5b7a7f2f38");
            var headOfDepartment = new Guid("b3c3ef89-fb6f-4cbe-8040-7c2f13d8c291");
            var evaluationCommitee = new Guid("93d6f134-dae2-4d84-a4a9-e391cc32ed55");
            var lecture = new Guid("8ec3ea62-fb1b-4a6c-bbb0-45ea56a21827");
            var admin = new Guid("8ec3ea62-fb1b-4a6c-bbb0-45ea56a21828");

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = academicAffairsOffice,
                    RoleName = "Academic Affairs Office",
                },
                new Role
                {
                    Id = studentRole,
                    RoleName = "Student",
                },
                new Role
                {
                    Id = headOfDepartment,
                    RoleName = "Head of Department",
                },
                new Role
                {
                    Id = evaluationCommitee,
                    RoleName = "Evaluation Committee",
                },
                new Role
                {
                    Id = lecture,
                    RoleName = "Lecture",
                },
                new Role
                {
                    Id = admin,
                    RoleName ="Admin"
                }
            );
        }
    }
}
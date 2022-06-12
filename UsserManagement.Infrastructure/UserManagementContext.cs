using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;
using UserManagement.Domain.Core;
using UsserManagement.Infrastructure.EntityTypeConfiguration;

namespace UsserManagement.Infrastructure
{
    public class UserManagementContext : DbContext, IUnitOfWork
    {
        public const string SchemaName = "UserManagement";

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }


    }
}

using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;
using UsserManagement.Infrastructure.EntityTypeConfiguration;

namespace UsserManagement.Infrastructure
{
    public class UserManagementContext : DbContext
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

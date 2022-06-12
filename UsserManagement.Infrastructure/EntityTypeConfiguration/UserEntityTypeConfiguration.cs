using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain;

namespace UsserManagement.Infrastructure.EntityTypeConfiguration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            //b.HasKey(e => e.Id);
            b.ToTable("User", UserManagementContext.SchemaName);

            b.Property(e => e.Username).IsRequired();
            b.Property(e => e.Password).IsRequired();
            b.Property(e => e.Salt).IsRequired();
        }
    }
}

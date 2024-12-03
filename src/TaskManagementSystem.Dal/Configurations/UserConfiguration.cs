using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Dal.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.UserName)
                .HasMaxLength(100);

            builder
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .Property(u => u.Email)
                .HasMaxLength(255);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}

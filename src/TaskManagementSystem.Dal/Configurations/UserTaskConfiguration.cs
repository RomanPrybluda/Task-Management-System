using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Entities;

namespace TMS.Dal.Configurations
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder
                .HasKey(ut => ut.Id);

            builder
                 .Property(ut => ut.Title)
                 .IsRequired()
                 .HasMaxLength(30);

            builder
                 .Property(ut => ut.Description)
                 .HasMaxLength(100);

            builder
                 .HasOne<User>()
                 .WithMany()
                 .HasForeignKey(ut => ut.UserId)
                 .IsRequired();

        }
    }
}
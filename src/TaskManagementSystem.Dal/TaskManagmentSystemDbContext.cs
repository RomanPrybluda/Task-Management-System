using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Dal
{
    public class TaskManagmentSystemDbContext : DbContext
    {
        public TaskManagmentSystemDbContext(DbContextOptions options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagmentSystemDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Dal;
using TaskManagementSystem.Dal.Seeds;

namespace TaskManagementSystem.Api
{
    public static class DatabaseMigrationExtensions
    {
        public static void DatabaseMigrate(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskManagmentSystemDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                var userInitializer = new UserInitializer(context);
                userInitializer.InitializeUsers();

                var userTaskInitializer = new UserTaskInitializer(context);
                userTaskInitializer.InitializeUserTasks();
            }
        }
    }
}

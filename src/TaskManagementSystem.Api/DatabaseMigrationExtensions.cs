using Microsoft.EntityFrameworkCore;
using TMS.Dal;
using TMS.Dal.Seeds;

namespace TMS.Api
{
    public static class DatabaseMigrationExtensions
    {
        public static async Task DatabaseMigrate(this IServiceProvider serviceProvider)
        {
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskManagmentSystemDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    await context.Database.MigrateAsync();
                }

                var userInitializer = new UserInitializer(context);
                userInitializer.InitializeUsers();

                var userTaskInitializer = new UserTaskInitializer(context);
                userTaskInitializer.InitializeUserTasks();
            }
        }


    }
}

using Bogus;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Dal.Seeds
{
    public class UserInitializer
    {
        private readonly TaskManagmentSystemDbContext context;

        private readonly Dictionary<string, string> userCredentials = new Dictionary<string, string>
        {
            { "JohnDoe", "Password1!" },
            { "JaneSmith", "Password2@" },
            { "MichaelJohnson", "Password3#" },
            { "EmilyDavis", "Password4$" },
            { "ChrisBrown", "Password5%" },
            { "SarahMiller", "Password6^" },
            { "DavidWilson", "Password7&" },
            { "JessicaMoore", "Password8*" },
            { "MatthewTaylor", "Password9(" },
            { "AshleyAnderson", "Password0)" }
        };

        public UserInitializer(TaskManagmentSystemDbContext context)
        {
            this.context = context;
        }

        public void InitializeUsers()
        {
            if (context.Users.Count() >= 10)
                return;

            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.UserName, (f, u) => GetUserName())
                .RuleFor(u => u.Email, (f, u) => $"{u.UserName.ToLower()}+{f.Random.Int(1000, 9999)}@example.com")
                .RuleFor(u => u.PasswordHash, (f, u) => HashPassword(userCredentials[u.UserName]))
                .RuleFor(u => u.CreatedAt, f => f.Date.Past())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent());

            int usersToCreate = 10 - context.Users.Count();
            var newUsers = new List<User>();

            foreach (var user in userCredentials)
            {
                if (context.Users.Any(u => u.UserName == user.Key))
                    continue;

                var newUser = faker.Generate();
                newUser.UserName = user.Key;
                newUser.PasswordHash = HashPassword(user.Value);
                newUsers.Add(newUser);
                if (newUsers.Count >= usersToCreate)
                    break;
            }

            if (newUsers.Any())
            {
                context.Users.AddRange(newUsers);
                context.SaveChanges();
            }
        }

        private string GetUserName()
        {
            return userCredentials.Keys.FirstOrDefault();
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}

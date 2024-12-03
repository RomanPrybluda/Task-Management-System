using Bogus;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Dal.Seeds
{
    public class UserTaskInitializer
    {
        private readonly TaskManagmentSystemDbContext context;

        public UserTaskInitializer(TaskManagmentSystemDbContext context)
        {
            this.context = context;
        }

        public void InitializeUserTasks()
        {
            string[] titles = new string[]
            {
                "Task A", "Task B", "Task C", "Task D", "Task E", "Task F", "Task G", "Task H", "Task I", "Task J",
                "Task K", "Task L", "Task M", "Task N", "Task O", "Task P", "Task Q", "Task R", "Task S", "Task T",
                "Task U", "Task V", "Task W", "Task X", "Task Y", "Task Z", "Task 1", "Task 2", "Task 3", "Task 4",
                "Task 5", "Task 6", "Task 7", "Task 8", "Task 9", "Task 10", "Task 11", "Task 12", "Task 13", "Task 14",
                "Task 15", "Task 16", "Task 17", "Task 18", "Task 19", "Task 20", "Task 21", "Task 22", "Task 23", "Task 24",
                "Task 25", "Task 26", "Task 27", "Task 28", "Task 29", "Task 30"
            };

            string[] descriptions = new string[]
            {
                "Buy table tomorrow.",
                "Meet with Janny on Friday.",
                "Prepare the report for the meeting.",
                "Call the client about the new contract.",
                "Send the email to the marketing team.",
                "Finish the presentation by 5 PM.",
                "Review the project budget with John.",
                "Fix the bug in the login system.",
                "Update the website content.",
                "Schedule a meeting with the design team.",
                "Review the new design proposal.",
                "Write the documentation for the API.",
                "Check the server logs for errors.",
                "Organize the team lunch on Friday.",
                "Test the new feature for the app.",
                "Submit the final version of the report.",
                "Call the supplier to confirm delivery.",
                "Plan the next sprint with the team.",
                "Create a list of tasks for the week.",
                "Send the contract draft to the lawyer.",
                "Prepare the meeting room for the presentation.",
                "Follow up with the customer on their request.",
                "Update the project timeline in the system.",
                "Complete the code review for the pull request.",
                "Prepare the demo for the client meeting.",
                "Organize the project files in the cloud.",
                "Check the email for urgent requests.",
                "Fix the typo on the website.",
                "Schedule a call with the project manager.",
                "Review the latest sales report.",
                "Send the invoice to the client.",
                "Prepare the budget forecast for the next quarter.",
                "Order the office supplies from the vendor.",
                "Check the system for security vulnerabilities.",
                "Create a backup of the database.",
                "Test the login functionality on mobile.",
                "Review the user feedback for improvements.",
                "Update the project status in the dashboard.",
                "Confirm the meeting time with the client.",
                "Send an invitation for the team-building event.",
                "Prepare the agenda for the meeting.",
                "Review the marketing strategy with the team.",
                "Create a plan for the upcoming project.",
                "Follow up with the design team on changes.",
                "Update the product documentation.",
                "Write an email to the customer support team.",
                "Test the application on different devices.",
                "Check the project progress with the client.",
                "Send the report to the stakeholders.",
                "Create a plan for the next product release."
            };

            if (context.UserTasks.Count() >= 50)
                return;

            var faker = new Faker<UserTask>()
                .RuleFor(ut => ut.Id, f => Guid.NewGuid())
                .RuleFor(ut => ut.Title, (f, ut) => titles[f.Random.Int(0, titles.Length - 1)])
                .RuleFor(ut => ut.Description, (f, ut) => descriptions[f.Random.Int(0, descriptions.Length - 1)])
                .RuleFor(ut => ut.DueDate, f => f.Date.Future())
                .RuleFor(ut => ut.Status, f => f.PickRandom<UserStatus>())
                .RuleFor(ut => ut.Priority, f => f.PickRandom<UserTaskPriority>())
                .RuleFor(ut => ut.UserId, f => f.PickRandom(context.Users.ToList()).Id);

            var users = context.Users.ToList();

            int existingTasksCount = context.UserTasks.Count();
            int tasksToCreate = 50 - existingTasksCount;

            for (int i = 0; i < tasksToCreate; i++)
            {
                var userTasks = faker.Generate();

                context.UserTasks.Add(userTasks);
            }

            context.SaveChanges();
        }
    }
}


# Task Management System

.NET backend service that manages a task management system with user authentication.
## Overview
The Task Management System is a backend service built with .NET Core 8 that allows users to manage their tasks efficiently. Users can register, authenticate, and manage their tasks, including creating, updating, and deleting tasks.
## Prerequisites
* **Visual Studio**: An integrated development environment (IDE) from Microsoft. Make sure you have Visual Studio 2022 or later installed. The Community edition is free and sufficient for this project.
* **.NET SDK**: .NET 8 SDK is required. You can download it from the [.NET Download page](https://dotnet.microsoft.com/download/dotnet/8.0).
* **MS SQL Server**: Microsoft SQL Server for the database. You can use SQL Server Express for local development.
## Installation
**Clone the repository**:
   ```sh
   git clone https://github.com/RomanPrybluda/Task-Management-System.git
   ```
## Configuration

The application uses an `appsettings.json` file for configuration. 
Ensure you update the connection string and JWT settings to match your environment.

``` json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagementSystem;Trusted_Connection=True;"
  },
  "JWT": {
    "Issuer": "http://localhost:5000",
    "Audience": "http://localhost:5000",
    "SigninKey": "YourSecureSigninKey"
  }
}
```

## How To Run

1. **Open Solution**:
    - Open the solution in Visual Studio.
2. **Set Startup Project**:
    - Set the `TaskManagementSystem` project as the Startup Project and build the project.
3. **Configure Database**:
    - Ensure the connection string in `appsettings.json` is correctly configured for your database.
4. **Run the Application**:
    - Run the application.

## Endpoints

- **POST `api/users/register`**: Register a new user.
- **POST `api/users/loginWithUserName`**: Authenticate a user with username and get a JWT.
- **POST `api/users/loginWithUserEmail`**: Authenticate a user with email and get a JWT.
- **POST `api/users/changePassword`**: Change the user’s password.
- **POST `api/tasks`**: Create a new task (authenticated).
- **GET `api/tasks`**: Retrieve a list of tasks for the authenticated user, with optional filters (e.g., status, due date, priority).
- **GET `api/tasks/{id}`**: Retrieve the details of a specific task by its ID (authenticated).
- **PUT `api/tasks/{id}`**: Update an existing task (authenticated).
- **DELETE `api/tasks/{id}`**: Delete a specific task by its ID (authenticated).
## Database Migrations

When you run the application, the database will be automatically created with the necessary tables (`User` and `UserTasks`). The database will also be seeded with initial data.
## Testing

The project includes unit tests to ensure the functionality of the application.
## API Documentation

The application uses Swagger for API documentation. After running the application.

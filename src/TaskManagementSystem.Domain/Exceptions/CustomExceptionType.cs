namespace TMS.Domain.Exceptions
{
    public enum CustomExceptionType
    {
        NoContent = 0,
        UserNotFound = 1,
        TaskNotFound = 2,
        TasksNotFound = 3,
        UserAlreadyExist = 4,
        InternalError = 5,
        UserCreationFailed = 6
    }
}

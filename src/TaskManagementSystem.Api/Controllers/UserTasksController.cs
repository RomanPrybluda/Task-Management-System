using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.DTO.Pagination;
using TaskManagementSystem.Domain.DTO.Request;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tasks")]

    public class UserTasksController : ControllerBase
    {
        private readonly IUserTaskService userTaskService;

        public UserTasksController(IUserTaskService taskService)
        {
            this.userTaskService = taskService;
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpGet]
        public async Task<IActionResult> GetTasksAsync(
            [Required] Guid userId,
            [FromQuery] GetUserTasksRequest getUserTasksRequest,
            [FromQuery] PaginationRequest paginationRequest)
        {
            var userTasks = await userTaskService.GetUserTasksAsync(userId, getUserTasksRequest, paginationRequest);
            return Ok(userTasks);
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTask([Required] Guid userTaskId, [Required] Guid userId)
        {
            var userTask = await userTaskService.GetUserTaskByIdAsync(userTaskId, userId);
            return Ok(userTask);
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpPost]
        public async Task<IActionResult> CreateUserTask([FromBody] CreateUserTaskRequest task, [Required] Guid userId)
        {
            var createdTask = await userTaskService.CreateUserTaskAsync(task, userId);
            return Ok(createdTask);
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateUserTask([FromBody] UpdateUserTaskRequest task, [Required] Guid userId)
        {
            var updatedTask = await userTaskService.UpdateUserTaskAsync(task, userId);
            return Ok(updatedTask);
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUserTask([Required] Guid taskId, [Required] Guid userId)
        {
            await userTaskService.DeleteUserTaskAsync(taskId, userId);
            return NoContent();
        }
    }

}

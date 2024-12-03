using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Domain.Abstractions;
using TaskManagementSystem.Domain.Auth.DTO;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("users")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([Required] Register request)
        {
            var result = await accountService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [SwaggerResponse(200, "REQUEST_SUCCESSFUL", typeof(OkResult))]
        [SwaggerResponse(400, "BAD_REQUEST", typeof(ErrorResponse))]
        [SwaggerResponse(500, "INTERNAL_SERVER_ERROR5", typeof(ErrorResponse))]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([Required] LoginRequest request)
        {
            var result = await accountService.LoginAsync(request);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }
    }
}

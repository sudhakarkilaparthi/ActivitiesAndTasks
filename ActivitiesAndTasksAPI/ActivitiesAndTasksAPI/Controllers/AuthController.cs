using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Helpers;
using ActivitiesAndTasksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesAndTasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthModel _authModel;
        public AuthController(AuthModel authModel)
        {
            _authModel = authModel;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto input)
        {
            ApiReturnResponse result = await _authModel.Login(input);
            return ApiResponseHelper.BuildResponse(result, this);
        }

        [HttpPost("GoogleLogin")]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleLoginRequest input)
        {
            ApiReturnResponse result = await _authModel.GoogleLogin(input);
            return ApiResponseHelper.BuildResponse(result, this);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto input)
        {
            // Implement login logic here
            return Ok();
        }
    }
}

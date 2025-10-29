using Microsoft.AspNetCore.Mvc;
using ReEV.Service.Auth.DTOs;
using ReEV.Service.Auth.Services;

namespace ReEV.Service.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register ([FromBody] UserCreateDTO registerDto)
        {
            try
            {
                var newUser = await _authService.Register(registerDto);

                return CreatedAtAction(
                    nameof(UsersController.GetUserById), 
                    "Users",
                    new { id = newUser.Id },
                    newUser);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDto)
        {
            try
            {
                var loginResponse = await _authService.Login(loginDto);
                return Ok(loginResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}

using Cyra.Data;
using Cyra.Models;
using Cyra.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cyra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterModel request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario registrado de manera correcta", response));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ExceptionResponse(ex));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginModel request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario registrado de manera correcta", response));
            }
            catch (Exception ex)
            {
                return Unauthorized(ApiResponseHelper.ExceptionResponse(ex));
            }
        }
    }
}

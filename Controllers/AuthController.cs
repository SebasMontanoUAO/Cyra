using Cyra.Data;
using Cyra.Models;
using Cyra.Services.Interfaces;
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
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error en el registro",
                    Details = ex.Message // si quieres mostrar solo el mensaje
                };
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Error al registrar el usuario", errorResponse));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginModel request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Login exitoso", response));
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error en el login",
                    Details = ex.Message
                };
                return Unauthorized(ApiResponseHelper.GetResponse(ResponseType.Failure, "Error en el login", errorResponse));
            }
        }
    }
}

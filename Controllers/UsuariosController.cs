using Cyra.Data;
using Cyra.Models;
using Cyra.Repositories;
using Cyra.Repositories.Interfaces;
using Cyra.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cyra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var response = usuarios.Select(u => new UsuarioResponseModel
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Email = u.Email,
                Telefono = u.Telefono,
                Direccion = u.Direccion,
                FechaCreacion = u.FechaCreacion,
                Estado = u.Estado.ToString(),
                TipoUsuario = u.TipoUsuario
            });

            return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuarios obtenidos correctamente!", response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null) return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound, $"No se pudo encontrar el usuario ID: {id}"));

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario encontrado correctamente!", new UsuarioResponseModel
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Email = usuario.Email,
                    Telefono = usuario.Telefono,
                    Direccion = usuario.Direccion,
                    FechaCreacion = usuario.FechaCreacion,
                    Estado = usuario.Estado.ToString(),
                    TipoUsuario = usuario.TipoUsuario
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ExceptionResponse(ex));
            }
        }

        // POST api/usuarios/register
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioRegisterModel usuarioRegistrado)
        {
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(usuarioRegistrado.Email);
            if (usuarioExistente != null) return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Email ya registrado.", usuarioRegistrado));

            try
            {
                var response = await _authService.RegisterAsync(usuarioRegistrado);
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario registrado de manera correcta", response));
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error en el registro",
                    Details = ex.Message
                };
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Error al registrar el usuario", errorResponse));
            }
        }

        // PUT: api/usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UsuarioUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Model State is not valid", ModelState));

            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound, $"No se encontró el usuario con Id {id}"));

            usuario.Nombre = model.Nombre;
            usuario.Telefono = model.Telefono;
            usuario.Direccion = model.Direccion;
            usuario.Estado = model.Estado;

            try
            {
                var actualizado = await _usuarioRepository.UpdateAsync(usuario);

                var response = new UsuarioResponseModel
                {
                    IdUsuario = actualizado.IdUsuario,
                    Nombre = actualizado.Nombre,
                    Email = actualizado.Email,
                    Telefono = actualizado.Telefono,
                    Direccion = actualizado.Direccion,
                    Estado = actualizado.Estado.ToString()
                };

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario actualizado correctamente!", response));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ExceptionResponse(ex));
            }
            
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var eliminado = await _usuarioRepository.DeleteAsync(id);
                if (!eliminado)
                {
                    return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound, "No se ha podido encontrar el usuario que está intentando eliminar."));
                }
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, $"Se eliminó el usuario con ID {id}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ExceptionResponse(ex));
            }
        }
    }
}

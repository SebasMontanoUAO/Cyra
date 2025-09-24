using Cyra.Data;
using Cyra.Models;
using Cyra.Repositories;
using Cyra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyra.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

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
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioRegisterModel usuarioRegistro)
        {
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(usuarioRegistro.Email);
            if (usuarioExistente != null)
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "El email ya está registrado."));

            var usuario = new Usuario
            {
                Nombre = usuarioRegistro.Nombre,
                Email = usuarioRegistro.Email,
                Telefono = usuarioRegistro.Telefono,
                Direccion = usuarioRegistro.Direccion,
                TipoUsuario = usuarioRegistro.TipoUsuario
            };

            try
            {
                var nuevoUsuario = await _usuarioRepository.AddAsync(usuario);

                var responseModel = new UsuarioResponseModel
                {
                    IdUsuario = nuevoUsuario.IdUsuario,
                    Nombre = nuevoUsuario.Nombre,
                    Email = nuevoUsuario.Email,
                    Telefono = nuevoUsuario.Telefono,
                    Direccion = nuevoUsuario.Direccion,
                    FechaCreacion = nuevoUsuario.FechaCreacion,
                    Estado = nuevoUsuario.Estado.ToString(),
                    TipoUsuario = nuevoUsuario.TipoUsuario
                };

                var apiResponse = ApiResponseHelper.GetResponse(ResponseType.Success, "Usuario creado exitosamente", responseModel);

                return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.IdUsuario }, apiResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

using Cyra.Data;
using Cyra.Models;
using Cyra.Repositories;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            return Ok(new UsuarioResponseModel
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                Direccion = usuario.Direccion,
                FechaCreacion = usuario.FechaCreacion,
                Estado = usuario.Estado.ToString(),
                TipoUsuario = usuario.TipoUsuario
            });
        }

        // POST api/usuarios/register
        [HttpPost("register")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioRegisterModel usuarioRegistro)
        {
            var usuarioExistente = await _usuarioRepository.GetByEmailAsync(usuarioRegistro.Email);
            if (usuarioExistente != null)
                return BadRequest("El email ya está registrado.");

            var usuario = new Usuario
            {
                Nombre = usuarioRegistro.Nombre,
                Email = usuarioRegistro.Email,
                Telefono = usuarioRegistro.Telefono,
                Direccion = usuarioRegistro.Direccion,
                TipoUsuario = usuarioRegistro.TipoUsuario
            };

            var nuevoUsuario = await _usuarioRepository.AddAsync(usuario);

            return CreatedAtAction(nameof(GetById), new { id = nuevoUsuario.IdUsuario }, new UsuarioResponseModel
            {
                IdUsuario = nuevoUsuario.IdUsuario,
                Nombre = nuevoUsuario.Nombre,
                Email = nuevoUsuario.Email,
                Telefono = nuevoUsuario.Telefono,
                Direccion = nuevoUsuario.Direccion,
                FechaCreacion = nuevoUsuario.FechaCreacion,
                Estado = nuevoUsuario.Estado.ToString(),
                TipoUsuario = nuevoUsuario.TipoUsuario
            });
        }
    }
}

using Cyra.Data;
using Cyra.Models;
using Cyra.Repositories;
using Cyra.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using System.Text;

namespace Cyra.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        public async Task<AuthResponseModel> LoginAsync(UsuarioLoginModel request)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(request.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                throw new Exception("Credenciales inválidas.");

            return GenerateAuthResponse(usuario);
        }

        public async Task<AuthResponseModel> RegisterAsync(UsuarioRegisterModel request)
        {
            var existing = await _usuarioRepository.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new Exception("El correo ya está registrado.");

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                Telefono = request.Telefono,
                Direccion = request.Direccion,
                TipoUsuario = request.TipoUsuario,
                Estado = "ACTIVO",
                FechaCreacion = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _usuarioRepository.AddAsync(usuario);

            return GenerateAuthResponse(usuario);
        }

        private AuthResponseModel GenerateAuthResponse(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new AuthResponseModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                TipoUsuario = usuario.TipoUsuario
            };
        }
    }
}

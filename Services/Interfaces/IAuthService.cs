using Cyra.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Cyra.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> RegisterAsync(UsuarioRegisterModel request);
        Task<AuthResponseModel> LoginAsync(UsuarioLoginModel request);
    }
}

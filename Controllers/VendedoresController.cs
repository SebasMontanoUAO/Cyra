using Cyra.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cyra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorRepository _vendedorRepository;

        public VendedoresController(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vendedores = await _vendedorRepository.GetAllAsync();
                var response = vendedores.Select({

                });
            }
        }
    }
}

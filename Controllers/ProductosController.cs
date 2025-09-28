using Cyra.Data;
using Cyra.Models;
using Cyra.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cyra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductosController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productos = await _productoRepository.GetAllAsync();
                var response = productos.Select(p => new ProductoResponseModel
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    EstadoPublicacion = p.EstadoPublicacion,
                    FechaPublicacion = p.FechaPublicacion,
                    IdVendedor = p.IdVendedor,
                    NombreVendedor = p.Vendedor.NombreNegocio
                });

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Productos obtenidos de manera correcta", response));
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto == null) return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound,
                                                      $"No se ha podido encontrar el producto con el ID: {id}"));

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Se ha encontrado el producto!", new ProductoResponseModel
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    EstadoPublicacion = producto.EstadoPublicacion,
                    FechaPublicacion = producto.FechaPublicacion,
                    IdVendedor = producto.IdVendedor,
                    NombreVendedor = producto.Vendedor.NombreNegocio
                }));
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

        //[Authorize(Roles = "VENDEDOR")]
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreateModel productoCreado)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = productoCreado.Nombre,
                    Descripcion = productoCreado.Descripcion,
                    Precio = productoCreado.Precio,
                    Stock = productoCreado.Stock,
                    IdVendedor = productoCreado.IdVendedor
                };

                var response = await _productoRepository.AddAsync(producto);
                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Producto creado!", response));
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "Ocurrió un error en el registro",
                    Details = ex.Message
                };
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Error al registrar el producto", errorResponse));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(long id, ProductoUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseHelper.GetResponse(ResponseType.Failure, "Model State is not valid", ModelState));

            var producto = await _productoRepository.GetByIdAsync(id);
            if (producto == null) return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound,
                                                  $"No se encontró el producto con ID: {id}"));

            producto.Nombre = model.Nombre;
            producto.Descripcion = model.Descripcion;
            if (model.Precio.HasValue)
                producto.Precio = model.Precio.Value;
            if (model.Stock.HasValue)
                producto.Stock = model.Stock.Value;

            try
            {
                var actualizado = await _productoRepository.UpdateAsync(producto);

                var response = new ProductoResponseModel
                {
                    IdProducto = actualizado.IdProducto,
                    Nombre = actualizado.Nombre,
                    Descripcion = actualizado.Descripcion,
                    Precio = actualizado.Precio,
                    Stock = actualizado.Stock,
                    EstadoPublicacion = actualizado.EstadoPublicacion,
                    FechaPublicacion = actualizado.FechaPublicacion,
                    IdVendedor = actualizado.IdVendedor,
                    NombreVendedor = actualizado.Vendedor.NombreNegocio
                };

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, "Producto actualizado corractamente!", response));
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

        [Authorize(Roles = "VENDEDOR")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            try
            {
                var eliminado = await _productoRepository.DeleteAsync(id);
                if (!eliminado) return NotFound(ApiResponseHelper.GetResponse(ResponseType.NotFound, $"No se pudo encontrar el producto con ID: {id}"));

                return Ok(ApiResponseHelper.GetResponse(ResponseType.Success, $"Se elimino el producto con ID: {id}"));
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
    }
}

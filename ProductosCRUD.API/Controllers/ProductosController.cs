using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductosCRUD.API.DTOs;
using ProductosCRUD.Business.Services;
using ProductosCRUD.Data.Models;

namespace ProductosCRUD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación para todos los endpoints
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _productoService;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ProductoService productoService, ILogger<ProductosController> logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los productos activos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosLosProductos();
                var productosDto = productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    FechaCreacion = p.FechaCreacion,
                    Activo = p.Activo
                });
                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerProductoPorId(id);
                if (producto == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                var productoDto = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    FechaCreacion = producto.FechaCreacion,
                    Activo = producto.Activo
                };

                return Ok(productoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CreateProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = crearProductoDto.Nombre,
                    Descripcion = crearProductoDto.Descripcion,
                    Precio = crearProductoDto.Precio,
                    Stock = crearProductoDto.Stock
                };

                var productoCreado = await _productoService.CrearProducto(producto);

                var productoDto = new ProductoDto
                {
                    Id = productoCreado.Id,
                    Nombre = productoCreado.Nombre,
                    Descripcion = productoCreado.Descripcion,
                    Precio = productoCreado.Precio,
                    Stock = productoCreado.Stock,
                    FechaCreacion = productoCreado.FechaCreacion,
                    Activo = productoCreado.Activo
                };

                return CreatedAtAction(nameof(GetProducto), new { id = productoDto.Id }, productoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDto>> UpdateProducto(int id, [FromBody] ActualizarProductoDto actualizarProductoDto)
        {
            try
            {
                if (id != actualizarProductoDto.Id)
                {
                    return BadRequest("El ID de la URL no coincide con el ID del producto");
                }

                var producto = new Producto
                {
                    Id = actualizarProductoDto.Id,
                    Nombre = actualizarProductoDto.Nombre,
                    Descripcion = actualizarProductoDto.Descripcion,
                    Precio = actualizarProductoDto.Precio,
                    Stock = actualizarProductoDto.Stock
                };

                var productoActualizado = await _productoService.ActualizarProducto(producto);

                var productoDto = new ProductoDto
                {
                    Id = productoActualizado.Id,
                    Nombre = productoActualizado.Nombre,
                    Descripcion = productoActualizado.Descripcion,
                    Precio = productoActualizado.Precio,
                    Stock = productoActualizado.Stock,
                    FechaCreacion = productoActualizado.FechaCreacion,
                    Activo = productoActualizado.Activo
                };

                return Ok(productoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Elimina (desactiva) un producto
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            try
            {
                var resultado = await _productoService.EliminarProducto(id);
                if (!resultado)
                {
                    return NotFound($"Producto con ID {id} no encontrado");
                }

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Busca productos por término
        /// </summary>
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> BuscarProductos([FromQuery] string termino)
        {
            try
            {
                var productos = await _productoService.BuscarProductos(termino);
                var productosDto = productos.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    FechaCreacion = p.FechaCreacion,
                    Activo = p.Activo
                });

                return Ok(productosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}

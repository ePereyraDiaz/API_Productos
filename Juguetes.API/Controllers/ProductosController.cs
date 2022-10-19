using Juguetes.Domain.DTO.General;
using Juguetes.Domain.Models;
using Juguetes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Juguetes.API.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService ProductosService;

        public ProductosController(IProductosService ProductosService_)
        {
            ProductosService = ProductosService_;
        }

        /// <summary>
        /// Productos Actuales en la base de datos (localhost)
        /// </summary>
        /// <returns>Listta de pproductos</returns>
        [HttpGet("/api/Productos/ObtenerProductos")]
        public async Task<IActionResult> ObtenerProductos()
        {
            try
            {
                var productos = await ProductosService.ObtenerProductos();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + " \nDetalles:  " + ex.Message.ToString());
            }
        }

		/// <summary>
		/// Agrega un Nuevo producto a la base de datos
		/// </summary>
		/// <param name="model">Recibe el total de parametros que necesita la tabla Productos</param>
		/// <returns>Generic response: bitResultado y vchMensaje</returns>
		[HttpPost("/api/Productos/AgregarProducto")]
        public async Task<IActionResult> AgregarProducto([FromBody] MantenimientoProductos model)
        {
            try
            {
                var productos = await ProductosService.AgregarProducto(model);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + " \nDetalles:  " + ex.Message.ToString());
            }
        }

		/// <summary>
		/// Actualiza Producto existente en la base de datos
		/// </summary>
		/// <param name="id">Id del producto</param>
		/// <param name="model">Recibe modelo completo  para Actualizarlo</param>
		/// <returns>Generic response: bitResultado y vchMensaje</returns>
		[HttpPut("/api/Productos/ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] MantenimientoProductos model)
        {
            try
            {
                var productos = await ProductosService.EditarProducto(id, model);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + " \nDetalles:  " + ex.Message.ToString());
            }
        }

		/// <summary>
		/// Eliminaciòn de pproductos  por Id
		/// </summary>
		/// <param name="id">Id del producto</param>
		/// <returns>Generic response: bitResultado y vchMensaje</returns>
		[HttpDelete("/api/Productos/EliminarProducto")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var productos = await ProductosService.EliminarProductos(id);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + " \nDetalles:  " + ex.Message.ToString());
            }
        }
    }
}

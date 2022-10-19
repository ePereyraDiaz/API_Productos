using Juguetes.Domain.Models;
using Juguetes.Persistence.Dapper;
using Juguetes.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Juguetes.Persistence.DB_Context;
using Juguetes.Domain.DTO.General;
using Microsoft.EntityFrameworkCore;

namespace Juguetes.Services.Implementations
{
	public class ProductosService : IProductosService
	{
		private DapperHelper_ORM Dapper_ { get; }
		private readonly ApiDBContext _context;

		public ProductosService(IDapper_ORM Dapper, IConfiguration config, ApiDBContext context)
		{
			Dapper_ = new DapperHelper_ORM(Dapper) ?? throw new ArgumentNullException(nameof(Dapper));
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<bool> ProductoInvalido(int id)
		{
			var Producto = await _context.Productos.Where(b => b.Id == id).Select(x => x).FirstOrDefaultAsync();
			return Producto == null;
		}

		public async Task<IList<Productos>> ObtenerProductos()
		{
			try
			{
				var obj = new { };
				var response = await Dapper_.GetAllAsync<Productos, object>(
				   "prObtenerProductos", obj
				   );

				return response;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<GenericResponseDTO> AgregarProducto(MantenimientoProductos model)
		{
			try
			{
				model.Movimiento = 1;
				var response = await Dapper_.GetAsync<GenericResponseDTO, MantenimientoProductos>(
				   "prMantenimientoProductos", model
				   );

				return response;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<GenericResponseDTO> EditarProducto(int id, MantenimientoProductos model)
		{
			try
			{
				if (await ProductoInvalido(id))
				{
					return new GenericResponseDTO { bitResultado = false, vchMensaje = "Se proporcionó un id de producto incorrecto, verificar" };
				}

				model.Movimiento = 2;
				model.Id = id;
				var response = await Dapper_.GetAsync<GenericResponseDTO, MantenimientoProductos>(
				   "prMantenimientoProductos", model
				   );

				return response;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<GenericResponseDTO> EliminarProductos(int id)
		{
			try
			{
				if (await ProductoInvalido(id))
				{
					return new GenericResponseDTO { bitResultado = false, vchMensaje = "Se proporcionó un id de producto incorrecto, verificar" };
				}

				var objModel = new MantenimientoProductos{ Movimiento = 3, Id = id };

				var response = await Dapper_.GetAsync<GenericResponseDTO, MantenimientoProductos>(
				   "prMantenimientoProductos", objModel
				   );

				return response;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

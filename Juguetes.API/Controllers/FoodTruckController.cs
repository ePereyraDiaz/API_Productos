using Juguetes.Domain.DTO.General;
using Juguetes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juguetes.API.Controllers
{
	[ApiController]
	public class FoodTruckController : ControllerBase
	{
		private readonly IFoodTruckService FoodTruckService;

		public FoodTruckController(IFoodTruckService FoodTruckService_)
		{
			FoodTruckService = FoodTruckService_;
		}

		/// <summary>
		/// Realiza  peticion a una API publica FoodTruck
		/// </summary>
		/// <param name="DayOrder">Dìa de la semana (1 a 7)</param>
		/// <param name="HourOrder">Hora del dìa</param>
		/// <returns></returns>
		[HttpGet("/api/FoodTruck/ObtenerFoodTrucks")]
		public async Task<IActionResult> ObtenerFoodTrucks(int DayOrder, DateTime HourOrder)
		{
			try
			{
				var productos = await FoodTruckService.ObtenerFoodTrucks(DayOrder, HourOrder);
				return Ok(productos);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + " \nDetalles:  " + ex.Message.ToString());
			}
		}
	}
}

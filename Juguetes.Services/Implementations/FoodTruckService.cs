using Juguetes.Domain.Models;
using Juguetes.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Services.Implementations
{
	public class FoodTruckService : IFoodTruckService
	{
		/// <summary>
		/// Peticion a  FoodTruck
		/// </summary>
		/// <param name="DayOrder">Dia de la semana</param>
		/// <param name="HourOrder">Hora del dia</param>
		/// <returns></returns>
		public async Task<IList<FoodTruck>> ObtenerFoodTrucks(int DayOrder, DateTime HourOrder)
		{
			try
			{
				using (var request = new HttpRequestMessage())
				{
					var httpClient = new HttpClient();

					// Construccion de la peticion
					request.Content = new StringContent("");
					request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
					request.Method = new HttpMethod("GET");
					request.RequestUri = new Uri($"https://data.sfgov.org/resource/jjew-r69b.json?dayorder={DayOrder}", UriKind.RelativeOrAbsolute);
					request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

					// Respuesta
					var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
					var responseText = await response.Content.ReadAsStringAsync();

					// Respuesta
					var Obj_Resp = JsonConvert.DeserializeObject<IList<FoodTruck>>(responseText);

					//Filtrado por hora {01/01/2022 12:00:00 a. m.}
					IList<FoodTruck> FoodTruckFiltered = Obj_Resp.Where(item => HourOrder.Hour >= item.starttime.Hour && HourOrder.Hour <= item.endtime.Hour).ToList();

					return FoodTruckFiltered;
				}
			}
			catch (Exception Ex)
			{
				throw;
			}
		}
	}
}

using Juguetes.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Services.Interfaces
{
	public interface IFoodTruckService
	{
		Task<IList<FoodTruck>> ObtenerFoodTrucks(int DayOrder, DateTime HourOrder);
	}
}

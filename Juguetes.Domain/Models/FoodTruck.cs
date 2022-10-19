using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Domain.Models
{
	public class FoodTruck
	{
		public string applicant { get; set; } = String.Empty;
		public string location { get; set; } = String.Empty;
		public int locationid { get; set; }
		public DateTime starttime { get; set; }
		public DateTime endtime { get; set; }
		public string dayorder { get; set; } = String.Empty;
	}
}

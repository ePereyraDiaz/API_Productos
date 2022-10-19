using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string vchNombre { get; set; } = String.Empty;
        public string vchCorreo { get; set; } = String.Empty;
        public bool bitActivo { get; set; }
        public DateTime dtmFechaModificacion { get; set; }
    }
}

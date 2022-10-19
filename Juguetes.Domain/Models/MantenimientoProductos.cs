using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Domain.Models
{
    public class MantenimientoProductos
    {
        public int Movimiento { get; set; }
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int RestriccionEdad { get; set; }
        public string? Compania { get; set; }
    }
}

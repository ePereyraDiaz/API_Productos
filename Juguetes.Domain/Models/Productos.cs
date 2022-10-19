using System;
namespace Juguetes.Domain.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int RestriccionEdad { get; set; }
        public string? Compania { get; set; }
    }
}

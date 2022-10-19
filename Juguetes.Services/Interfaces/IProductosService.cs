using Juguetes.Domain.DTO.General;
using Juguetes.Domain.Models;

namespace Juguetes.Services.Interfaces
{
    public interface IProductosService
    {
        Task<IList<Productos>> ObtenerProductos();
        Task<GenericResponseDTO> AgregarProducto(MantenimientoProductos model);
        Task<GenericResponseDTO> EditarProducto(int id, MantenimientoProductos model);
        Task<GenericResponseDTO> EliminarProductos(int id);
    }
}

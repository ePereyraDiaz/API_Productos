using Juguetes.Domain.DTO.General;

namespace Juguetes.Domain.Models
{
    public class spLoginUsuario : GenericResponseDTO
    {
        public int Id { get; set; }
        public string vchNombre { get; set; } = string.Empty;
        public string vchCorreo { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}

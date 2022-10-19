using Juguetes.Domain.Models;

namespace Juguetes.Services.Interfaces
{
    public interface ILoginService
    {
        Task<spLoginUsuario> LoginUsuario(string vchCorreo);
    }
}

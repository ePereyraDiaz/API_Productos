using Juguetes.Domain.Models;
using Juguetes.Services.Interfaces;

namespace Juguetes.Services.Implementations
{
    public class LoginService : ILoginService
    { 
        public async Task<spLoginUsuario> LoginUsuario(string vchCorreo)
        {
            try
            {
                return new spLoginUsuario { 
                  bitResultado = true,
                  Id = 10,
                  vchCorreo  = vchCorreo,
                  vchNombre = vchCorreo,
                  vchMensaje  = ""
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Juguetes.Domain.Models;

namespace Juguetes.Services.Interfaces
{
    public interface IAuth_JWT
    {
        public Token_JWT GenerarToken(string vchCorreo);
    }
}

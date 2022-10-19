using Juguetes.Domain.DTO.General;
using Juguetes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Juguetes.API.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService LoginService_;
        private readonly IAuth_JWT auth_JWT_;

        public LoginController(ILoginService Login, IAuth_JWT auth_JWT)
        {
            LoginService_ = Login;
            auth_JWT_ = auth_JWT;
        }

        /// <summary>
        /// Autentica al usuario sin hacer ppeticion a la base de datos
        /// Utiliza Token en las peticiones para productos y FoodTruck
        /// El correo puede ser cualquier texto y el Token caduca en 12 horas
        /// </summary>
        /// <param name="vchCorreo">Correo del Usuario</param>
        /// <returns>Token y datos de usuario</returns>
        [HttpGet("/api/Login/Autenticacion")]
        public async Task<IActionResult> Autenticacion(string vchCorreo)
        {
            try
            {
                var usuario = await LoginService_.LoginUsuario(vchCorreo);
                if (usuario.bitResultado)
                {
                    var token = auth_JWT_.GenerarToken(usuario.vchCorreo);
                    usuario.Token = token.Token;
                    usuario.Expires = token.Expires;
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Fecha y Hora: " + LocalDatetime.Now() + "\n" + HttpStatusCodesMessages.StatusCode500 + "\nUsuario: " + vchCorreo + " \nDetalles:  " + ex.Message.ToString());
            }
        }
    }
}

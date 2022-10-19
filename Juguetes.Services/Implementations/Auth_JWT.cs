using Juguetes.Domain.Models;
using Juguetes.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Juguetes.Services.Implementations
{
    public class Auth_JWT : IAuth_JWT
    {
        private readonly IConfiguration _configuration;

        public Auth_JWT(IConfiguration config)
        {
            _configuration = config;
        }

        public Token_JWT GenerarToken(string vchCorreo)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
              );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                    new Claim(ClaimTypes.Email, vchCorreo),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 2 horas.
                    expires: DateTime.UtcNow.AddHours(12)
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new Token_JWT { Token = new JwtSecurityTokenHandler().WriteToken(_Token), Expires = _Token.ValidTo };
        }
    }
}

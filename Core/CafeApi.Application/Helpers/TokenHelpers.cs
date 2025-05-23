using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CafeApi.Application.Dtos.AuthDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CafeApi.Application.Helpers
{
    public class TokenHelpers
    {
        private  readonly IConfiguration _configuration;
        public TokenHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //token oluşturma işlemi
        public string GenerateToken(TokenDto dto)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //burada kullanıcı bilgilerini direkt olarak alacagız

            var claims = new List<Claim>
            {
                new Claim("_e" , dto.Email),
                new Claim("_u" , dto.Id),
                new Claim("_r" ,dto.Role ),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], //CafeAPI
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), //tokenin ne kadar süre geçerli olacagı
                signingCredentials: credentials // tokenin imzalanması için gerekli bilgiler
            );

            var resultToken = new JwtSecurityTokenHandler().WriteToken(token);
            //burada tokeni döndürecegiz
            return resultToken;
        }
    }
}

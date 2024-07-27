using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Name, User.DisplayName),
            };

            var UserRole = await userManager.GetRolesAsync(User);
            foreach (var Role in UserRole)
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokendescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(AuthClaims),
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"],
                Expires = DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                SigningCredentials = credentials,
            };

            var Token = new JwtSecurityTokenHandler();
            var token = Token.CreateToken(tokendescriptor);
            return Token.WriteToken(token);
        }
    }
}

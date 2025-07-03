using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Models.DTOs;
using Server.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Repo.repositories
{
    public class JwtRepository
    {
        private readonly JwtOptions _jwtOptions;
        private UserManager<ApplicationUser> _userManager;

        public JwtRepository(JwtOptions jwtOptions, UserManager<ApplicationUser> userManager)
        {
            _jwtOptions = jwtOptions;
            _userManager = userManager;
        }
        public async Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));

            var tokenHandler = new JwtSecurityTokenHandler();
            // Most Important Claims
            var claims = new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier,applicationUser.Id),
                    new(ClaimTypes.Name,applicationUser.UserName),
                    new(ClaimTypes.Email,applicationUser.Email),
                    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            // if we will add Roles later
            var roles = await _userManager.GetRolesAsync(applicationUser);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // responsible for writing info in Token to be right when validating it in TokenValidationParameters
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256),


                // until here this is For Token info then we will add User Info to know who is this user
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor); // here we created Token but as Class Object via tokenHandler 
            var accessToken = tokenHandler.WriteToken(securityToken); // here we will Convert it from Object to Compact Format "String"
            return accessToken;
        }
    }
}

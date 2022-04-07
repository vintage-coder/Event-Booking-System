using EBSystem.Authentication.API.Entities;
using EBSystem.Authentication.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EBSystem.Authentication.API.Handlers
{
    public class JWTHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _JWTSettings;
		private readonly UserManager<ApplicationUser> _userManager;



		public JWTHelper(IConfiguration configuration,
			UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;

            _configuration = configuration;

            _JWTSettings = _configuration.GetSection("JWTSettings");
        }

		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_JWTSettings.GetSection("securityKey").Value);
			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private async Task<List<Claim>> GetClaims(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email)
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				issuer: _JWTSettings.GetSection("validIssuer").Value,
				audience: _JWTSettings.GetSection("validAudience").Value,
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(_JWTSettings.GetSection("expiryInMinutes").Value)),
				signingCredentials: signingCredentials);

			return tokenOptions;
		}

		public async Task<string> GenerateToken(ApplicationUser users)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims(users);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return token;
		}

	}
}

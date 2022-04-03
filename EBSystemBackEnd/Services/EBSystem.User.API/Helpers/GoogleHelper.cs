using Google.Apis.Auth;
using EBSystem.Authentication.API.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using EBSystem.Authentication.API.Models;
using Microsoft.AspNetCore.Identity;

namespace EBSystem.Authentication.API.Helpers
{
    public class GoogleHelper
    {

		private readonly IConfiguration _configuration;
		private readonly IConfigurationSection _goolgeSettings;
		private readonly IConfigurationSection _jwtSettings;

		private readonly UserManager<ApplicationUser> _userManager;




		public GoogleHelper(IConfiguration configuration,
			UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;

			_configuration = configuration;

			_jwtSettings = configuration.GetSection("JWTSettings");	
			_goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
		}


        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }



        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
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


		public async Task<string> GenerateToken(ApplicationUser users)
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims(users);
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return token;
		}


		public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
		{
			try
			{
				var settings = new GoogleJsonWebSignature.ValidationSettings()
				{
					Audience = new List<string>() { _goolgeSettings.GetSection("clientId").Value }
				};

				var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
				return payload;
			}
			catch (Exception ex)
			{
				
				return null;
			}
		}

	}
}

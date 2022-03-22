using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EBSystem.Authentication.API.DBContexts;
using EBSystem.Authentication.API.Contracts;
using EBSystem.Authentication.API.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace EBSystem.Authentication.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticateContext _authenticateContext;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(ITokenRepository tokenRepository, AuthenticateContext authenticateContext)
        {
            _tokenRepository=tokenRepository;
            _authenticateContext=authenticateContext;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login == null)
            {
                return BadRequest("Invalid Client Request");
            }

            var user = _authenticateContext.logins.FirstOrDefault(u => (u.UserName==login.UserName) 
            && (u.Password==u.Password));

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var accessToken = _tokenRepository.GenerateAccessToken(claims);
            var refreshToken = _tokenRepository.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(10);
            _authenticateContext.SaveChanges();
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiDto tokenApi)
        {
            if (tokenApi is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApi.AccessToken;
            string refressToken = tokenApi.RefreshToken;

            var principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = _authenticateContext.logins.SingleOrDefault(u => u.UserName == username);


            if (user == null || user.RefreshToken != refressToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid Client Request");
            }


            var newAccessToken = _tokenRepository.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenRepository.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            _authenticateContext.SaveChanges();

            return new OkObjectResult(new
            {
                accessToken = newAccessToken,
                refressToken = newRefreshToken
            });

        }


        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _authenticateContext.logins.SingleOrDefault(u => u.UserName == username);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _authenticateContext.SaveChanges();
            return NoContent();
        }

    }
}

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
using Microsoft.AspNetCore.Identity;
using EBSystem.Authentication.API.Models;
using EBSystem.Authentication.API.Entities;

namespace EBSystem.Authentication.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticateContext _authenticateContext;
        private readonly ITokenRepository _tokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;





        public AuthController(ITokenRepository tokenRepository, AuthenticateContext authenticateContext,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenRepository=tokenRepository;
            _authenticateContext=authenticateContext;

            _jwtSettings = _configuration.GetSection("JWTSettings");
        }


        [HttpPost]
        [Route("login")]
        public async  Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var user=await _userManager.FindByNameAsync(login.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password)) ;
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));


                }

                var authSignKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value));


                var token = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256));


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.UserName
                });

            }
            return Unauthorized();

           
        }







        //[HttpPost, Route("login")]
        //public IActionResult Login([FromBody] LoginDto login)
        //{
        //    if (login == null)
        //    {
        //        return BadRequest("Invalid Client Request");
        //    }

        //    var user = _authenticateContext.logins.FirstOrDefault(u => (u.UserName==login.UserName) 
        //    && (u.Password==u.Password));

        //    if (user == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, login.UserName),
        //        new Claim(ClaimTypes.Role, "Manager")
        //    };

        //    var accessToken = _tokenRepository.GenerateAccessToken(claims);
        //    var refreshToken = _tokenRepository.GenerateRefreshToken();
        //    user.RefreshToken = refreshToken;
        //    user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(10);
        //    _authenticateContext.SaveChanges();
        //    return Ok(new
        //    {
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken
        //    });
        //}

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiDto tokenApi)
        {
            if (tokenApi is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApi.AccessToken;
            string refreshToken = tokenApi.RefreshToken;

            var principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = _authenticateContext.logins.SingleOrDefault(u => u.UserName == username);


            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
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
                refreshToken = newRefreshToken
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

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var userExist = await _userManager.FindByNameAsync(register.UserName) ;

            if(userExist!=null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Status", Message = "User already Exists" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp=Guid.NewGuid().ToString(),
                UserName=register.UserName,
            };


            var result=await _userManager.CreateAsync(user,register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Status", Message = "User not created successfully" });
            }


            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

                return Ok(new Response { Status = "Success", Message = "User Created Successfully" });

        }



        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto register)
        {
            var userExist = await _userManager.FindByNameAsync(register.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Status", Message = "User already Exists" });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName,
            };


            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Status", Message = "User not created successfully" });
            }


            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User Created Successfully" });

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EBSystem.Authentication.API.Dtos;
using EBSystem.Authentication.API.Helpers;
using EBSystem.Authentication.API.Models;
using EBSystem.Authentication.API.Handlers;
using EBSystem.Authentication.API.Entities;

namespace EBSystem.Authentication.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GoogleAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly GoogleHelper _googleHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTHelper _jwtHandler;



        public GoogleAuthController(IConfiguration configuration, 
            GoogleHelper googleHelper, 
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            _configuration = configuration;

            _googleHelper = googleHelper;

            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
        }

        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            var payload = await _googleHelper.VerifyGoogleToken(externalAuth);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");
            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);


            //_userManager.
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
           
          

            
            //var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new ApplicationUser { Email = payload.Email, UserName = payload.Email };

                    await _userManager.CreateAsync(user);


                    await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            if (user == null)
                return BadRequest("Invalid External Authentication.");

            var token = await _googleHelper.GenerateToken(user);
            return Ok(new { Token = token, IsAuthSuccessful = true });

        }


    }
}

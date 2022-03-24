using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBSystem.Authentication.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _goolgeSettings;

        public GoogleAuthController(IConfiguration configuration)
        {
            _configuration = configuration;

            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
        }
    }
}

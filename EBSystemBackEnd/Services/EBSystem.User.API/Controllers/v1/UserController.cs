using EBSystem.Authentication.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace EBSystem.User.API.Controllers.v1
{

    [Authorize(Roles =UserRoles.Admin)]
    [ApiVersion("1.0")]
    [Route("ebs/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {

            _logger.LogInformation($"User Controller get action method was called at {DateTime.Now}");

            return Ok( new
            {
                ID = 1,
                Name = "Gowtham",
                Age = 25
            });
        }
    
    }
}

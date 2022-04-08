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
        
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok( new
            {
                ID = 1,
                Name = "Gowtham",
                Age = 25
            });
        }
    
    }
}

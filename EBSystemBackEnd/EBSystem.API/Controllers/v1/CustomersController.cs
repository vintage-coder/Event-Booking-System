using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBSystem.API.Controllers.v1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("ebs/v{version:apiVersion}/[controller]")]     
    public class CustomersController : ControllerBase
    {
        [HttpGet,Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "Gowthaman", "Srivatson" };
        }
    }
}

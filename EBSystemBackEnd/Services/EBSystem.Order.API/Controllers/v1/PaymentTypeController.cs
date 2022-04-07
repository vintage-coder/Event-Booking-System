using Microsoft.AspNetCore.Mvc;



namespace EBSystem.Order.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("ebs/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        // GET: api/<PaymentTypeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PaymentTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PaymentTypeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PaymentTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaymentTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

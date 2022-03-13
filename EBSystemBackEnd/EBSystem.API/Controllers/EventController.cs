using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using EBSystem.Services.Contracts;
using EBSystem.Models.Models;

namespace EBSystem.API.Controllers
{

    [Route("ebs/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;


        public EventController(IEventService eventService)
        {
            _eventService = eventService;

        }


        [HttpGet("getallevents")]
        public async Task<IActionResult> Get()
        {

            try
            {
                return Ok(await _eventService.GetEvents());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error retrieving data from the database");
            }


        }

        // GET api/<ValuesController>/5
        [HttpGet("geteventbyid/{id}")]
        public async Task<ActionResult<EventTbl>> Get(int id)
        {
            try
            {
                var result = await _eventService.GetEvent(id);

                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return result;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


    }
}















        // POST api/<ValuesController>
        //[HttpPost("addevent")]
        //public Task<ActionResult<EventTbl>> Post(EventTbl eventTbl)
        //{
        //    try
        //    {
        //        if(eventTbl==null)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error retrieving data from the database");
        //    }
        //}

        // PUT api/<ValuesController>/5
        //[HttpPut("updateevent/{id}")]
        //public Task<ActionResult<EventTbl> Put(int id, EventTbl value)
        //{
        //    return null;
        //}

    // DELETE api/<ValuesController>/5
    //[HttpDelete("deleteevent/{id}")]
    //public async Task<ActionResult<EventTbl>> Delete(int id)
    //{
    //    try
    //    {
    //        var eventtodelete = await _eventService.GetEvent(id);

    //        if (eventtodelete == null)
    //        {
    //            return NotFound($"Event with ID={id} Not found");
    //        }

    //        return await _eventService.DeleteEvent(id);
    //    }
    //    catch (Exception)
    //    {

    //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data");
    //    }
    //}
      

    //}



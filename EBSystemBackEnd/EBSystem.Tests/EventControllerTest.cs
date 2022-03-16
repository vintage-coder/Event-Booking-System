using Xunit;
using EBSystem.API.Controllers.v1;
using EBSystem.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using EBSystem.Models.Models;
using System.Collections.Generic;
using EBSystem.Models.DBContexts;
using EBSystem.Services.Services;


namespace EBSystem.Tests
{
    public class EventControllerTest
    {
        private readonly EventController _eventController;

        private readonly IEventService _eventService;

        private readonly EMSDBContext _context;

        public EventControllerTest()
        {
            _context = new EMSDBContext();
            _eventService = new EventService(_context);
            
            _eventController = new EventController(_eventService);
        }

        [Fact]
        public void Get_When_Called_ReturnsOkResule()
        {
            var okResult = _eventController.Get(); 
            // Assert
            var items = Assert.IsType<OkObjectResult>(okResult.Result);


            Assert.IsType<OkObjectResult>(items);
        }
    }
}
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using EBSystem.Services.Contracts;
using Moq;
using EBSystem.Tests.MockData;
using EBSystem.API.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace EBSystem.Tests.System.Controllers
{
    public class EventControllerTest
    {
      

        public EventControllerTest()
        {
           
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            var eventService = new Mock<IEventService>();

            eventService.Setup(_ => _.GetEvents()).ReturnsAsync(EventData.GetAllEvents());


            var sut = new EventController(eventService.Object);


            var result = await sut.Get();


            result.GetType().Should().Be(typeof(OkObjectResult));

            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
       
    }
}
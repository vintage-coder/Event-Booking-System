using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBSystem.Models.DBContexts;
using Microsoft.EntityFrameworkCore;
using Xunit;
using EBSystem.Tests.MockData;
using EBSystem.Services.Services;
using FluentAssertions;

namespace EBSystem.Tests.System.Services
{
    public class EventServiceFake : IDisposable
    {

        private readonly EMSDBContext _dbcontext;

        public EventServiceFake()
        {
            var options = new DbContextOptionsBuilder<EMSDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _dbcontext=new EMSDBContext(options);

            _dbcontext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllEvents_ReturnEventCollection()
        {

            // Arrange

            _dbcontext.EventTbls.AddRange(EventData.GetAllEvents());
            _dbcontext.SaveChanges();
            var sut = new EventService(_dbcontext);

            //Act

            var result = await sut.GetEvents();


            //Asssert

            result.Should().HaveCount(EventData.GetAllEvents().Count);

        }
        public void Dispose()
        {
            //
            _dbcontext.Database.EnsureDeleted();

            _dbcontext.Dispose();
        }
    }
}

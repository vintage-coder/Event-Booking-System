using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBSystem.Models.Models;
using EBSystem.Services.Contracts;

namespace EBSystem.Tests
{
    public class EventServiceFake:IEventService
    {

        private readonly IEnumerable<EventTbl> _events;
        public EventServiceFake()
        {
            _events= new List<EventTbl>()
            {
                new EventTbl()
                { 
                    EventId=1,
                    EventName="Plays" ,
                    EndDate=new DateTime(22,3,2022),
                    StartDate=new DateTime(22,4,2022),
                    NoOfTickets=2000,
                },
                new EventTbl()
                {
                    EventId=2,
                    EventName="Cricket" ,
                    EndDate=new DateTime(22,3,2022),
                    StartDate=new DateTime(22,4,2022),
                    NoOfTickets=5000,
                }
            };
        }

        public Task<EventTbl> AddEvent(EventTbl eventTbl)
        {
            throw new NotImplementedException();
        }

        public Task<EventTbl> DeleteEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventTbl> GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventTbl>> GetEvents()
        {
            return (Task<IEnumerable<EventTbl>>)_events;
        }

        public Task<EventTbl> UpdateEvent(EventTbl eventTbl)
        {
            throw new NotImplementedException();
        }
    }
}

using EBSystem.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace EBSystem.Services.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<EventTbl>> GetEvents();
        Task<EventTbl> GetEvent(int id);

        Task<EventTbl> AddEvent (EventTbl eventTbl);

        Task<EventTbl> UpdateEvent( EventTbl eventTbl);

        Task<EventTbl> DeleteEvent(int id);

    }
}

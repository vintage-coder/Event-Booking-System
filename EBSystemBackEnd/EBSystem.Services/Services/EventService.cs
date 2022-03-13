using EBSystem.Models.Models;
using EBSystem.Services.Contracts;
using EBSystem.Models.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace EBSystem.Services.Services
{
    public class EventService : IEventService
    {
        private  EMSDBContext eMSDBContext;
        public EventService(EMSDBContext eMSDBContext)
        {
            this.eMSDBContext = eMSDBContext;
        }

        //public  EMSDBContext eMSDBContext = new EMSDBContext();

        public async Task<EventTbl> AddEvent(EventTbl eventTbl)
        {
            var result = await eMSDBContext.EventTbls.AddAsync(eventTbl);
            await eMSDBContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<EventTbl> DeleteEvent(int id)
        {
            var result = await eMSDBContext.EventTbls.FirstOrDefaultAsync(e => e.EventId == id);

            if(result!=null)
            {
                eMSDBContext.EventTbls.Remove(result);
                await eMSDBContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<EventTbl> GetEvent(int id)
        {
            var result= await eMSDBContext.EventTbls.FirstOrDefaultAsync(e => e.EventId == id);

            if(result!=null)
            {
                return result;
            }

            return null;
        }

        public async Task<IEnumerable<EventTbl>> GetEvents()
        {
            return await eMSDBContext.EventTbls.ToListAsync();
        }

        public async Task<EventTbl> UpdateEvent(EventTbl eventTbl)
        {
            var result = await eMSDBContext.EventTbls.FirstOrDefaultAsync(e => e.EventId == eventTbl.EventId);

            if(result!=null)
            {
               
                result.PromoCode = eventTbl.PromoCode;
                result.StartDate = eventTbl.StartDate;
                result.EventName = eventTbl.EventName;
                result.EndDate = eventTbl.EndDate;
                result.PromoCode=eventTbl.PromoCode;
                result.EventName=eventTbl.EventName;
                await eMSDBContext.SaveChangesAsync();

                return result;

            }

            return null;
           
        }
    }
}

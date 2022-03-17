using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBSystem.Models.Models;

namespace EBSystem.Tests.MockData
{
    public class EventData
    {
        public static List<EventTbl> GetAllEvents()
        {
            return new List<EventTbl>
            {

                new EventTbl()
                {
                    EventId = 1,
                    EventName = "Dance",
                    StartDate = new DateTime(2022, 03, 03),
                    EndDate = new DateTime(2022, 03, 29),
                    EventCategoryId = 1,
                    TicketCategoryId=1,
                    NoOfTickets =3000,
                    PromoCode ="RBEF43336C",
                    EventCategory=null,
                    TicketCategory=null,
                    //EventCategory = new EventCategoryTbl
                    //{
                    //    EventCategoryId=2,
                    //    EventCategoryName="Super Dance",
                    //    EventTbls=new List<EventTbl>(),
                    //},
                    //TicketCategory =new TicketCategoryTbl
                    //{
                    //    TicketCategoryId=2,
                    //    TicketCategoryName="Gold",
                    //    TicketPrice=5000,
                    //    EventTbls=new List<EventTbl>(),
                    //}
                },

                new EventTbl()
                {
                    EventId = 2,
                    EventName = "IPL",
                    StartDate = new DateTime(2022, 03, 03),
                    EndDate = new DateTime(2022, 03, 29),
                    EventCategoryId = 2,
                    TicketCategoryId=2,
                    NoOfTickets =1000,
                    PromoCode ="RBEF43336C",
                    EventCategory=null,
                    TicketCategory=null,
                    //EventCategory = new EventCategoryTbl
                    //{
                    //    EventCategoryId=2,
                    //    EventCategoryName="Sports",
                    //    EventTbls=new List<EventTbl>(),
                    //},
                    //TicketCategory =new TicketCategoryTbl
                    //{
                    //    TicketCategoryId=2,
                    //    TicketCategoryName="Premium",
                    //    TicketPrice=3000,
                    //    EventTbls=new List<EventTbl>(),
                    //}
                }
            };
        }
    }
}

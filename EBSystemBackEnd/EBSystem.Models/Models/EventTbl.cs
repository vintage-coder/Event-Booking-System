using System;
using System.Collections.Generic;

namespace EBSystem.Models.Models
{
    public partial class EventTbl
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EventCategoryId { get; set; }
        public int? TicketCategoryId { get; set; }
        public int? NoOfTickets { get; set; }
        public string? PromoCode { get; set; }

        public virtual EventCategoryTbl? EventCategory { get; set; }
        public virtual TicketCategoryTbl? TicketCategory { get; set; }
    }
}

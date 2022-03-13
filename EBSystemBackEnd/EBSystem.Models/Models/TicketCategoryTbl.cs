using System;
using System.Collections.Generic;

namespace EBSystem.Models.Models
{
    public partial class TicketCategoryTbl
    {
        public TicketCategoryTbl()
        {
            EventTbls = new HashSet<EventTbl>();
        }

        public int TicketCategoryId { get; set; }
        public string? TicketCategoryName { get; set; }
        public int? TicketPrice { get; set; }

        public virtual ICollection<EventTbl> EventTbls { get; set; }
    }
}

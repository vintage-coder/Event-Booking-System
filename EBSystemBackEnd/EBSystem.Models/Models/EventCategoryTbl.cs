using System;
using System.Collections.Generic;

namespace EBSystem.Models.Models
{
    public partial class EventCategoryTbl
    {
        public EventCategoryTbl()
        {
            EventTbls = new HashSet<EventTbl>();
        }

        public int EventCategoryId { get; set; }
        public string? EventCategoryName { get; set; }

        public virtual ICollection<EventTbl> EventTbls { get; set; }
    }
}

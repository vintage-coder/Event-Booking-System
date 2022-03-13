using System;
using System.Collections.Generic;

namespace EBSystem.Models.Models
{
    public partial class EventCategoryTbl
    {
        public int EventCategoryId { get; set; }
        public string? EventCategoryName { get; set; }

        public virtual EventTbl EventTbl { get; set; } = null!;
    }
}

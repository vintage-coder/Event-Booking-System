using System;
using System.Collections.Generic;

namespace EBSystem.Models.Models
{
    public partial class UserTbl
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}

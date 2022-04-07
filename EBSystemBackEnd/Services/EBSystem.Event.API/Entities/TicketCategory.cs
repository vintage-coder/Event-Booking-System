using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBSystem.Order.API.Entities
{
    public class TicketCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ticketCategoryId { get; set; }

        public string? ticketCategoryName { get; set; }

        public long? ticketPrice { get; set; }
       
    }
}

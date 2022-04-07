using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBSystem.Event.API.Entities
{
    public class EventCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long eventCategoryId { get; set; }

        public string? eventCategoryName { get; set; }
    }
}

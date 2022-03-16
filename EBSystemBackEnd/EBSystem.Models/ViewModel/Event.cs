namespace EBSystem.Models.ViewModel
{
    public class Event
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EventCategoryId { get; set; }
        public int? TicketCategoryId { get; set; }
        public int? NoOfTickets { get; set; }      
        public string? PromoCode { get; set; }
    }
}

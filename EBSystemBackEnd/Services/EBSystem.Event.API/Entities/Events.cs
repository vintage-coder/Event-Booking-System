using EBSystem.Order.API.Entities;

namespace EBSystem.Event.API.Entities
{
    public class Events
	{
        public long eventId { get; set; }

        public string? eventName { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public long? TotalTicket { get; set; }

        public EventCategory? eventCategory { get; set; }

        public TicketCategory? ticketCategory { get; set; }
	
    }
}

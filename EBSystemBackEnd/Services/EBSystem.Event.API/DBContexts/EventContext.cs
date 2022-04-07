using EBSystem.Event.API.Entities;
using EBSystem.Order.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBSystem.Event.API.DBContexts
{
    public class EventContext : DbContext
    {
        public EventContext()
        {

        }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {

        }
        public DbSet<Events> events { get; set; }

        public DbSet<EventCategory> eventCategories { get; set; }

        public DbSet<TicketCategory> ticketCategories { get; set; }


    }
}

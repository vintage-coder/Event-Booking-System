using EBSystem.Order.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBSystem.Order.API.DBContexts
{
    public class OrderContext:DbContext
    {
        public OrderContext()
        {

        }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<PaymentType> paymentTypes { get; set; }
    }
}

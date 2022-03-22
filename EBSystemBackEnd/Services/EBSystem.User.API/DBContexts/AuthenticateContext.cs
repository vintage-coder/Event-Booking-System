using EBSystem.Authentication.API.Entities;
using Microsoft.EntityFrameworkCore;
     


namespace EBSystem.Authentication.API.DBContexts
{
    public class AuthenticateContext:DbContext
    {
        public AuthenticateContext()
        {

        }

        public AuthenticateContext(DbContextOptions<AuthenticateContext> options):base(options)
        {

        }

        public DbSet<Login> logins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasData(new Login()
            {
                Id = 1,
                UserName = "Gowtham",
                Password = "Gowtham@123",
               
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Proiect_ASP_NET_MVC.Models;

namespace Proiect_ASP_NET_MVC.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) :
       base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<SoldCar> SoldCars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Brand>().ToTable("Brand");

            modelBuilder.Entity<Dealer>().ToTable("Dealer");
            modelBuilder.Entity<SoldCar>().ToTable("SoldCar");
            modelBuilder.Entity<SoldCar>()
            .HasKey(c => new { c.CarID, c.DealerID });  //cheia primara compusa!!
        }

    }


}

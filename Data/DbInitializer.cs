using Microsoft.EntityFrameworkCore;
using Proiect_ASP_NET_MVC.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Proiect_ASP_NET_MVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new
           ProjectContext(serviceProvider.GetRequiredService
            <DbContextOptions<ProjectContext>>()))
            {
                if (context.Cars.Any())
                {
                   return; // BD a fost creata anterior
                }
                

                var orders = new Order[]
                {
                new Order
                {
                    CarID = 32,
                    ClientID = 17,
                    OrderDate = DateTime.Parse("2021-02-25")},
                new Order
                {
                    CarID = 33,
                    ClientID = 18,
                    OrderDate = DateTime.Parse("2021-03-24")
                },
                new Order
                {
                    CarID = 34,
                    ClientID = 17,
                    OrderDate = DateTime.Parse("2021-01-20")
                },
                new Order
                {
                    CarID = 33,
                    ClientID = 18,
                    OrderDate = DateTime.Parse("2021-09-22")
                }
                };

                foreach (Order e in orders)
                {
                    context.Orders.Add(e);
                }
                context.SaveChanges();



                var dealers = new Dealer[]
                   {

                new Dealer
                {
                    DealerName = "Cheap Cars",
                    Adress = "Str. Plopilor, nr. 24",
                },
                new Dealer
                {
                    DealerName = "Best Cars",
                    Adress = "Str. Bucuresti, nr.45",
                },
                new Dealer
                {
                    DealerName = "Cars For Sale",
                    Adress = "Str. Aviatorilor, nr.5",
                }
                };

                foreach (Dealer d in dealers)
                {
                    context.Dealers.Add(d);
                }
                context.SaveChanges();


                var cars = context.Cars.Include(c=>c.Brand);
                var soldCars = new SoldCar[]
                    {
                new SoldCar
                {
                    CarID = cars.Single(c => c.Brand.Name == "Audi" && c.Model == "Q5").ID,
                    DealerID = dealers.Single(i => i.DealerName ==
                    "Best Cars").ID
                },
                new SoldCar
                {
                    CarID = cars.Single(c => c.Brand.Name == "Volvo" && c.Model == "XC90").ID,
                    DealerID = dealers.Single(i => i.DealerName ==
                    "Cheap Cars").ID
                }
                    };

                context.SaveChanges();
            }
        }
    }
}

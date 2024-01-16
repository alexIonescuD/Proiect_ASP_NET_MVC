using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_ASP_NET_MVC.Models
{
    public class Car
    {
        public int ID { get; set; }
        public int? BrandID { get; set; }
        public Brand? Brand { get; set; }
        public string Model { get; set; }

        public string Engine { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public ICollection<SoldCar>? SoldCars { get; set; }

    }
}

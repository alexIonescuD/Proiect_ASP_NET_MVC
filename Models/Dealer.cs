using System.ComponentModel.DataAnnotations;

namespace Proiect_ASP_NET_MVC.Models
{
    public class Dealer
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Dealer Name")]
        [StringLength(50)]
        public string DealerName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<SoldCar> SoldCars { get; set; }

    }
}

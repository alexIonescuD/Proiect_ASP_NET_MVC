namespace Proiect_ASP_NET_MVC.Models
{
    public class Brand
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Car>? Cars { get; set; }
    }
}

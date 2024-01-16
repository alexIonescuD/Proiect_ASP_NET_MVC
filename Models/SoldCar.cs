namespace Proiect_ASP_NET_MVC.Models
{
    public class SoldCar
    {
        public int DealerID { get; set; }
        public int CarID { get; set; }
        public Dealer Dealer { get; set; }
        public Car Car { get; set; }
    }
}

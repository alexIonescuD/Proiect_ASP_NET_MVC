namespace Proiect_ASP_NET_MVC.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

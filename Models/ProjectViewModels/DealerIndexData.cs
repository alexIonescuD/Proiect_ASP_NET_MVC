namespace Proiect_ASP_NET_MVC.Models.ProjectViewModels
{
    public class DealerIndexData
    {
        public IEnumerable<Dealer> Dealers { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

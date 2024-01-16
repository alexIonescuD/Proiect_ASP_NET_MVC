using System.ComponentModel.DataAnnotations;

namespace Proiect_ASP_NET_MVC.Models.ProjectViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int CarCount { get; set; }

    }
}

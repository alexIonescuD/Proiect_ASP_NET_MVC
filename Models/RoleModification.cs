using System.ComponentModel.DataAnnotations;

namespace Proiect_ASP_NET_MVC.Models
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[]? AddIds { get; set; } //  fara ? returneaza eroare in update
        public string[]? DeleteIds { get; set; } //  fara ? returneaza eroare in update

    }
}

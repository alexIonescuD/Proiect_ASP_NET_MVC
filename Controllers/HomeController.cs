using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proiect_ASP_NET_MVC.Data;
using Proiect_ASP_NET_MVC.Models;
using Proiect_ASP_NET_MVC.Models.ProjectViewModels;
using System.Diagnostics;
namespace Proiect_ASP_NET_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly ProjectContext _context;
        public HomeController(ProjectContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from order in _context.Orders
            group order by order.OrderDate into dateGroup
            select new OrderGroup()
            {
                OrderDate = dateGroup.Key,
                CarCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }


        public IActionResult Chat()
        {

            
            string currentUser = User.Identity.Name;
            ViewData["username"] = currentUser;
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
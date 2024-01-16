using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_ASP_NET_MVC.Data;
using Proiect_ASP_NET_MVC.Models;
using Proiect_ASP_NET_MVC.Models.ProjectViewModels;
namespace Proiect_ASP_NET_MVC.Controllers
{
    [Authorize(Policy = "OnlyDealers")]
    public class DealersController : Controller
    {
        private readonly ProjectContext _context;

        public DealersController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Dealers
        public async Task<IActionResult> Index(int? id, int? carID)
        {
            var viewModel = new DealerIndexData();
            viewModel.Dealers = await _context.Dealers
             .Include(i => i.SoldCars)
            .ThenInclude(i => i.Car)
            .ThenInclude(i => i.Brand)

            .Include(i => i.SoldCars)
            .ThenInclude(i => i.Car)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Client)
            
            .AsNoTracking()
            .OrderBy(i => i.DealerName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["DealerID"] = id.Value;
                Dealer dealer = viewModel.Dealers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Cars = dealer.SoldCars.Select(s => s.Car);
            }
            if (carID != null)
            {
                ViewData["CarID"] = carID.Value;
                viewModel.Orders = viewModel.Cars.Where(
                x => x.ID == carID).Single().Orders;
            }
            return View(viewModel);
        }


        // GET: Dealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dealers == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // GET: Dealers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DealerName,Adress")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dealer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dealer);
        }

        // GET: Dealers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dealer = await _context.Dealers
            .Include(i => i.SoldCars).ThenInclude(i => i.Car).ThenInclude(i => i.Brand)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (dealer == null)
            {
                return NotFound();
            }
            PopulateSoldCarData(dealer);
            return View(dealer);

        }
        private void PopulateSoldCarData(Dealer dealer)
        {
            var allCars = _context.Cars.Include(c=>c.Brand);
            var soldCars = new HashSet<int>(dealer.SoldCars.Select(c => c.CarID));
            var viewModel = new List<SoldCarData>();
            foreach (var car in allCars)
            {
                viewModel.Add(new SoldCarData
                {
                    CarID = car.ID,
                    Model = car.Model,
                    Brand = car.Brand,
                    IsSold = soldCars.Contains(car.ID)
                });
            }
            ViewData["Cars"] = viewModel;
        }

        // POST: Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedcars)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dealerToUpdate = await _context.Dealers
            .Include(i => i.SoldCars)
            .ThenInclude(i => i.Car)
           .ThenInclude(i => i.Brand)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Dealer>(
            dealerToUpdate,
            "",
            i => i.DealerName, i => i.Adress))
            {
                UpdateSoldCars(selectedcars, dealerToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSoldCars(selectedcars, dealerToUpdate);
            PopulateSoldCarData(dealerToUpdate);
            return View(dealerToUpdate);
        }
        private void UpdateSoldCars(string[] selectedcars, Dealer dealerToUpdate)
        {
            if (selectedcars == null)
            {
                dealerToUpdate.SoldCars = new List<SoldCar>();
                return;
            }
            var selectedcarsHS = new HashSet<string>(selectedcars);
            var SoldCars = new HashSet<int>
            (dealerToUpdate.SoldCars.Select(c => c.Car.ID));
            foreach (var Car in _context.Cars)
            {
                if (selectedcarsHS.Contains(Car.ID.ToString()))
                {
                    if (!SoldCars.Contains(Car.ID))
                    {
                        dealerToUpdate.SoldCars.Add(new SoldCar
                        {
                            DealerID =
                       dealerToUpdate.ID,
                            CarID = Car.ID
                        });
                    }
                }
                else
                {
                    if (SoldCars.Contains(Car.ID))
                    {
                        SoldCar CarToRemove = dealerToUpdate.SoldCars.FirstOrDefault(i
                       => i.CarID == Car.ID);
                        _context.Remove(CarToRemove);
                    }
                }
            }
        }

        // GET: Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dealers == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // POST: Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dealers == null)
            {
                return Problem("Entity set 'ProjectContext.Dealers'  is null.");
            }
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer != null)
            {
                _context.Dealers.Remove(dealer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerExists(int id)
        {
          return (_context.Dealers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

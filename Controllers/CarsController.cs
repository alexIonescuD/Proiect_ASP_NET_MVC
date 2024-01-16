using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_ASP_NET_MVC.Data;
using Proiect_ASP_NET_MVC.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Proiect_ASP_NET_MVC.Controllers
{

    [Authorize(Roles = "Employee")]
    public class CarsController : Controller
    {
        private readonly ProjectContext _context;

        public CarsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Cars
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandSortParm"] = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : ""; 
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["EngineSortParm"] = sortOrder == "Engine" ? "engine_desc" : "Engine";
            if (searchString != null) { 
                pageNumber = 1; 

            } else {

                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var cars = from b in _context.Cars select b;

            if (!String.IsNullOrEmpty(searchString)) {         //cautare dupa brand sau model

                cars = cars.Where(s => s.Brand.Name.Contains(searchString)
                                     || s.Model.Contains(searchString) 
                                     || s.Engine.Contains(searchString));  
            }

            switch (sortOrder)
            {
                case "brand_desc": cars = cars.OrderByDescending(b => b.Brand.Name); break;
                case "Price": cars = cars.OrderBy(b => b.Price); break;
                case "price_desc": cars = cars.OrderByDescending(b => b.Price); break;
                case "Engine": cars = cars.OrderBy(b => b.Engine); break;
                case "engine_desc": cars = cars.OrderByDescending(b => b.Engine); break;
                default:
                    cars = cars.OrderBy(b => b.Brand.Name);
                    break;
            }
            int pageSize = 3; 
            return View(await PaginatedList<Car>.CreateAsync(cars.AsNoTracking().Include(x => x.Brand), pageNumber ?? 1, pageSize));
            
        }

        // GET: Cars/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(x => x.Brand)
                    .Include(s => s.Orders)
                    .ThenInclude(e => e.Client)
                    .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {

            var brandsList = _context.Brands.Select(x => new
            {
                x.ID,
                x.Name
            });
            List<string> enginesList = new List<string> { "1.5", "1.6", "2.0", "2.2", "3.0" };
            ViewData["EngineID"] = new SelectList(enginesList);
            ViewData["BrandID"] = new SelectList(brandsList, "ID", "Name");

            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandID,Model,Engine,Price")] Car car)
        {
            try
            {

                if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }
            var brandsList = _context.Brands.Select(x => new
            {
                x.ID,
                x.Name
            });
            List<string> enginesList = new List<string> { "1.5", "1.6", "2.0", "2.2", "3.0" };
            ViewData["EngineID"] = new SelectList(enginesList);
            ViewData["BrandID"] = new SelectList(brandsList, "ID", "Name");

            var car = await _context.Cars.Include(x => x.Brand).FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carToUpdate = await _context.Cars.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Car>(
            carToUpdate,
            "",
            s => s.Brand, s => s.Model, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(carToUpdate);
        }


        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .AsNoTracking()
                .Include(x => x.Brand)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (car == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'ProjectContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Cars.Remove(car);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

using FinalBackend.Context;
using FinalBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
       

        private readonly RentDbContext _context;
        public CityController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<City>  cities = _context.Cities.Where(x => !x.IsDeleted)
                .ToList();
            return View(cities);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(city);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            City city = _context.Cities.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (city == null)
            {
                return RedirectToAction("Index", "Notfound");
            }


            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, City city)
        {
            City updatecity = _context.Cities.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (updatecity == null)
            {
                return RedirectToAction("Index", "Notfound");
            }

            updatecity.Name = city.Name;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            City city = _context.Cities.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            city.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

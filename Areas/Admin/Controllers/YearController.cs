using FinalBackend.Context;
using FinalBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class YearController : Controller
    {
        private readonly RentDbContext _context;
        public YearController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Year> years = _context.Years.Where(x => !x.IsDeleted).Include(x=>x.Brand)
                .ToList();
            return View(years);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Year year1)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(year1);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            Year updateyear = _context.Years.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (updateyear == null)
            {
                return RedirectToAction("Index", "Notfound");
            }


            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, Year year1)
        {
            Year updateyear = _context.Years.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (updateyear == null)
            {
                return RedirectToAction("Index", "Notfound");
            }

            updateyear.year = year1.year;
            updateyear.BrandId = year1.BrandId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Year updateyear = _context.Years.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            updateyear.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

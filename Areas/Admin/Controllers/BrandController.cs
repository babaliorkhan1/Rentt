using FinalBackend.Context;
using FinalBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class BrandController : Controller
    {

     
        private readonly RentDbContext _context;
        public BrandController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Brand> brands = _context.Brands.Where(x => !x.IsDeleted).Include(x=>x.Position)
                .ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            Brand brand = _context.Brands.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return RedirectToAction("Index", "Notfound");
            }


            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, Brand brand)
        {
            Brand updatebrand = _context.Brands.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (brand == null)
            {
                return RedirectToAction("Index", "Notfound");
            }

            updatebrand.Name = brand.Name;
            updatebrand.PositionId = brand.PositionId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Brand updatebrand = _context.Brands.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            updatebrand.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }

}

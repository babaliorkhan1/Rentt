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
    public class ModelController : Controller
    {
        private readonly RentDbContext _context;
        public ModelController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Model> models = _context.Models.Where(x => !x.IsDeleted).Include(x=>x.Brand)
                .ToList();
            return View(models);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Model model)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            Model model = _context.Models.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction("Index", "Notfound");
            }


            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, Model model)
        {
            Model updateModel = _context.Models.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (updateModel == null)
            {
                return RedirectToAction("Index", "Notfound");
            }

            updateModel.Name = model.Name;
            updateModel.BrandId= model.BrandId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Model updateModel = _context.Models.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            updateModel.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

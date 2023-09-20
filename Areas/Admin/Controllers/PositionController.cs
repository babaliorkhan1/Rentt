using FinalBackend.Context;
using FinalBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PositionController : Controller
    {
       

        private readonly RentDbContext _context;
        public PositionController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Position> positions = _context.Positions.Where(x=>!x.IsDeleted).ToList();  
            return View(positions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(position);
            _context.SaveChanges();     

            return RedirectToAction(nameof(Index));   
        }


        public IActionResult Update(int id)
        {

            Position position=_context.Positions.Where(x=>!x.IsDeleted &&x.Id==id).FirstOrDefault();    
            if (position==null)
            {
                return RedirectToAction("Index", "Notfound");
            }


            return View();
        }

        [HttpPost]
        public IActionResult Update(int id,Position position)
        {
            Position updatePosition = _context.Positions.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View();
            } 
            if (updatePosition==null)
            {
                return RedirectToAction("Index", "Notfound");
            }

            updatePosition.Name= position.Name;     
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Position position=_context.Positions.Where(x=>!x.IsDeleted).FirstOrDefault();    
            position.IsDeleted= true; 
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

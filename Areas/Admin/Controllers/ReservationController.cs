using FinalBackend.Context;
using FinalBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
     
        private readonly RentDbContext _context;
        public ReservationController(RentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Reservationn> reservations = _context.reservationOrders.Where(x => !x.IsDeleted).Include(x=>x.Brand).Include(x=>x.AppUser)
                .ToList();
            return View(reservations);
        }
    }
}

using FinalBackend.Context;
using FinalBackend.Models;
using FinalBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalBackend.Controllers
{
    public class CarController : Controller
    {
        private readonly RentDbContext _context;
        private readonly UserManager<AppUser> _UserManager;
        public CarController(RentDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _UserManager = userManager; 
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            settinguser settinguser = new settinguser();




            int totalCount = _context.Cars.Where(x => !x.IsDeleted).ToList().Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)totalCount / 8);
            ViewBag.CurrentPage = page;
            settinguser.cars = _context.Cars.Where(x => !x.IsDeleted).Include(x => x.ProductImages).Skip((int)(page - 1) * 8).Take(8).ToList();





            settinguser.setting = _context.settings.Where(x => !x.IsDeleted).FirstOrDefault();
            settinguser.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            settinguser.Years = _context.Years.Where(x => !x.IsDeleted && x.Id == x.BrandId).ToList();//yeari ortadan yoxa cixartmag ucun bu yola uz tutdum

            settinguser.Models = _context.Models.Where(x => !x.IsDeleted).ToList();

            if (User.Identity.IsAuthenticated)
            {
                settinguser.appUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            }


            settinguser.positions = _context.Positions.Where(x => !x.IsDeleted).ToList();

            return View(settinguser);
        }
        public IActionResult Detail(int id,int? dislike,int? like)
        {
            settinguser settinguser = new settinguser();
            if (dislike!=null)
            {
                Comment comment = _context.Comments.Where(x => !x.IsDeleted && x.CarId == id && x.Id == dislike).FirstOrDefault();
                if (comment.dislikecount==null)
                {
                    comment.dislikecount = 0;
                }
                comment.dislikecount+=1;
                _context.SaveChanges();

            }
            if (like != null)
            {
                Comment comment = _context.Comments.Where(x => !x.IsDeleted && x.CarId == id && x.Id == like).FirstOrDefault();
                if (comment.likecount1 == null)
                {
                    comment.likecount1 = 0;
                }
                comment.likecount1 += 1;
                _context.SaveChanges();

            }







            settinguser.comments = _context.Comments.Where(x => !x.IsDeleted && x.CarId == id).Include(x => x.AppUser).ThenInclude(x => x.Comments).ToList();

            settinguser.car = _context.Cars.Where(x => !x.IsDeleted && x.Id == id).Include(x => x.Position).Include(x => x.ProductImages)
                .FirstOrDefault();
           
            settinguser.cars = _context.Cars.Where(x => !x.IsDeleted).Include(x => x.Position).Include(x => x.ProductImages).Take(4).ToList();
            return View(settinguser);
        }
        [HttpPost]
        public async Task<IActionResult> GetFilteredCars(CarViewModel model1)
        {
            CarViewModel CarViewModel = new CarViewModel();

            //int totalCount = _context.Cars.Where(x => !x.IsDeleted).ToList().Count();
            //ViewBag.TotalPage = (int)Math.Ceiling((decimal)totalCount / 8);
            //ViewBag.CurrentPage = page;
            CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted).Include(x => x.ProductImages).ToList();


            CarViewModel.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted).ToList();
            CarViewModel.Years = _context.Years.Where(x => !x.IsDeleted &&x.BrandId==model1.BrandId).ToList();

            if (model1.PositionId != null && model1.BrandId == null && model1.ModelId == null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.PositionId == model1.PositionId).Include(x => x.ProductImages).ToList();

            }
            if (model1.PositionId != null && model1.BrandId != null && model1.ModelId == null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.PositionId == model1.PositionId && x.BrandId == model1.BrandId).Include(x => x.ProductImages).ToList();
                CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();

            }
            if (model1.PositionId != null && model1.BrandId != null && model1.ModelId != null )
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.PositionId == model1.PositionId && x.BrandId == model1.BrandId && x.ModelId == model1.ModelId).Include(x => x.ProductImages).ToList();
                CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
                CarViewModel.Years = _context.Years.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
            }
            if (model1.PositionId == null && model1.BrandId != null && model1.ModelId == null &&model1.YearId==null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).Include(x => x.ProductImages).ToList();
                CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
                CarViewModel.Years = _context.Years.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();

            }
            if (model1.PositionId == null && model1.BrandId == null && model1.ModelId != null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.ModelId == model1.ModelId).Include(x => x.ProductImages).ToList();

            }

            if (model1.PositionId != null && model1.BrandId == null && model1.ModelId != null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.PositionId == model1.PositionId && x.ModelId == model1.ModelId).Include(x => x.ProductImages).ToList();
                CarViewModel.Brands = _context.Brands.Where(x => !x.IsDeleted && x.PositionId == x.PositionId).ToList();
            }
            if (model1.PositionId == null && model1.BrandId != null && model1.ModelId != null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.ModelId == model1.ModelId && x.BrandId == model1.BrandId).Include(x => x.ProductImages).ToList();
                CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
            }
            if (model1.PositionId == null && model1.BrandId != null && model1.ModelId == null && model1.YearId != null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.YearId == model1.YearId &&x.BrandId==model1.BrandId).Include(x => x.ProductImages).ToList();
            }
            if (model1.PositionId == null && model1.BrandId == null && model1.ModelId == null && model1.YearId != null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.YearId == model1.YearId && x.YearId==model1.YearId).Include(x => x.ProductImages).ToList();
            }
            if (model1.PositionId != null && model1.BrandId != null && model1.ModelId != null &&model1.YearId!=null)
            {
                CarViewModel.cars = _context.Cars.Where(x => !x.IsDeleted && x.PositionId == model1.PositionId && x.BrandId == model1.BrandId && x.ModelId == model1.ModelId &&x.YearId==model1.YearId).Include(x => x.ProductImages).ToList();
                CarViewModel.Models = _context.Models.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
                CarViewModel.Years = _context.Years.Where(x => !x.IsDeleted && x.BrandId == model1.BrandId).ToList();
            }




            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                CarViewModel.appUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            }
            CarViewModel.setting = _context.settings.Where(x => !x.IsDeleted).FirstOrDefault();



            CarViewModel.positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            return View(CarViewModel);
        }
        public async Task<IActionResult> addbasket(int id)
        {
            Car? car = _context.Cars.Where(x => !x.IsDeleted &&x.Id==id).FirstOrDefault();

            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _UserManager.FindByNameAsync(User.Identity.Name);
                Favourite favourite1 = _context.Favourites.Where(x => !x.IsDeleted && x.Car == car &&x.AppUser==appUser).FirstOrDefault();
                if (favourite1==null)
                {
                   
                    Favourite favourite = new Favourite();
                    favourite.Car = car;
                    favourite.AppUser = appUser;
                    _context.Add(favourite);
                    _context.SaveChanges();
                    TempData["addbasket"] = "Baskete elave olundu";
                }
                else
                {
                    TempData["addbasket"] = "Elave olunub zaten";
                }
               

            }
            else
            {
                return RedirectToAction("login","account");
            }

            return RedirectToAction("index","home");




        }

        public async Task<IActionResult> GetAll()
        {

            if (User.Identity.IsAuthenticated && User.IsInRole("User"))
            {
                AppUser appUser = await _UserManager.FindByNameAsync(User.Identity.Name);


                IEnumerable<Favourite> favourites = _context.Favourites.Where(x => !x.IsDeleted && x.AppUser == appUser).Include(x => x.Car).ThenInclude(x => x.ProductImages).ToList(); 
                return View(favourites);

            }
            List<Favourite> favourites1 = default;
            return View(favourites1);
           


        }

        public async Task<IActionResult> DeleteBasket(int id)
        {

            if (User.Identity.IsAuthenticated)
            {
                Car? car = _context.Cars.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
                Favourite? favourite = _context.Favourites.Where(x => !x.IsDeleted && x.CarId == car.Id).FirstOrDefault();
                if (favourite == null)
                {
                    return RedirectToAction("index", "notfound");
                }
                favourite.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(GetAll));
        }







    }



}

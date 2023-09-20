using FinalBackend.Context;
using FinalBackend.Extensions;
using FinalBackend.Models;
using FinalBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace FinalBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CarController : Controller
    {
        private readonly RentDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMailService _MailService;
        public CarController(RentDbContext context, IWebHostEnvironment env,IMailService mailService)
        {
            _context = context;
            _env = env;
            _MailService= mailService;  
        }
      
        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.Where(x=>!x.IsDeleted).Include(x=>x.ProductImages)
                .ToList(); 
            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Positions=_context.Positions.Where(x=>!x.IsDeleted).ToList();   
            ViewBag.Brands=_context.Brands.Where(x=>!x.IsDeleted).ToList();   
            ViewBag.Models=_context.Models.Where(x=>!x.IsDeleted).ToList();
            ViewBag.Years=_context.Years.Where(x=>!x.IsDeleted).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Models = _context.Models.Where(x => !x.IsDeleted).ToList();
            ViewBag.Years = _context.Years.Where(x => !x.IsDeleted).ToList();


            if (!ModelState.IsValid)
            {
                return View(car);
            }

            int i = 0;
            foreach (var item in car.FormFiles)
            {
                ProductImage productImage = new ProductImage
                {
                    Image=item.CreateImage(_env.WebRootPath,"assets/Images"),
                    IsMain=i==0?true:false,
                    Car=car,

                };
                i++;
                _context.ProductImages.Add(productImage);
            }

           


          


         

            _context.Add(car);
            _context.SaveChanges();
            List<Subscribe> subscribes = _context.Subscribes.Where(x => !x.IsDeleted).ToList();
            string text = "salammm";
            foreach (var item in subscribes)
            {
                _MailService.Send1("pm2459179@gmail.com", item.Email,text,"NewCar");
            }


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            Car car=_context.Cars.Where(x=>!x.IsDeleted &&x.Id==id).FirstOrDefault();
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Models = _context.Models.Where(x => !x.IsDeleted).ToList();
            ViewBag.Years = _context.Years.Where(x => !x.IsDeleted).ToList();
            if (car==null)
            {
                return RedirectToAction("Index", "NotFound");
            }

            return View(car);   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id,Car car)
        {
            ViewBag.Positions = _context.Positions.Where(x => !x.IsDeleted).ToList();
            Car updateCar = _context.Cars.Where(x => !x.IsDeleted &&x.Id==id).FirstOrDefault();
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Models = _context.Models.Where(x => !x.IsDeleted).ToList();
            ViewBag.Years = _context.Years.Where(x => !x.IsDeleted).ToList();
            if (updateCar==null)
            {
                return RedirectToAction("Index", "NotFound");
            }


            if (!ModelState.IsValid)
            {
                return View(car);
            }
            if (car.FormFiles!=null)
            {
                foreach (var item in car.FormFiles)
                {
                    ProductImage productImage = new ProductImage
                    {
                        Image = item.CreateImage(_env.WebRootPath, "assets/Images"),
                        Car = updateCar,
                        IsMain = false,
                    };
                    _context.Add(productImage);       
                }
            }


            updateCar.Name=car.Name;
            updateCar.Year=car.Year;    
            updateCar.Price=car.Price;  
            updateCar.Info1 = car.Info1;    
            updateCar.Info2 = car.Info2;
            updateCar.Info3 = car.Info3;    
            updateCar.PriceInfo1= car.PriceInfo1;   
            updateCar.PriceInfo2= car.PriceInfo2;
            updateCar.PriceInfo3= car.PriceInfo3;   
            updateCar.ModelName=car.ModelName;
            updateCar.PositionId=car.PositionId;    
            updateCar.ModelId=car.ModelId;
            updateCar.BrandId = car.BrandId;
            updateCar.YearId=car.YearId;        


            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        public IActionResult Delete(int id)
        {
            Car car = _context.Cars.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            car.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

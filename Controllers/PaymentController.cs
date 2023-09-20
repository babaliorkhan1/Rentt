using FinalBackend.Context;
using FinalBackend.Extensions;
using FinalBackend.Models;
using FinalBackend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

namespace FinalBackend.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RentDbContext _context;
        private readonly UserManager<AppUser> _UserManager;
        public PaymentController(RentDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _UserManager = userManager;
        }

        public async Task<IActionResult> UserPayment(int id)
        {
            AppUser appUser =await  _UserManager.FindByNameAsync(User.Identity.Name);
            Car? car = _context.Cars.Where(x => !x.IsDeleted && x.Id == id).Include(x=>x.Brand).ThenInclude(x=>x.Cars).Include(x=>x.Model)
                .FirstOrDefault();
            PaymentViewModel paymentView = new PaymentViewModel();
            
            paymentView.BrandId = car.BrandId;
            paymentView.Brand = car.Brand;
            paymentView.ModelId= car.ModelId;
            paymentView.Model = car.Model;
            paymentView.Cities = _context.Cities.Where(x => !x.IsDeleted).ToList();
            paymentView.AppUser = appUser;
            paymentView.AppUserId = appUser.Id;
            paymentView.AppUser.Name = appUser.Name;
            paymentView.AppUser.SurName = appUser.SurName;
           

          
           
            return View(paymentView);

        }

        [HttpPost]
        public async Task<IActionResult> UserPayment(PaymentViewModel paymentView,int id)
        {
            AppUser appUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            Car? car = _context.Cars.Where(x => !x.IsDeleted && x.Id == id).Include(x => x.Brand).ThenInclude(x => x.Cars).Include(x => x.Model)
              .FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View(paymentView);
            }
            car.Isreservation = true;
            Reservationn reservationOrder=new Reservationn();
          
            reservationOrder.Name = appUser.Name;
            reservationOrder.Surname = appUser.SurName;
            reservationOrder.Email = appUser.Email;
            reservationOrder.Address = paymentView.Address;  
            reservationOrder.AppUser = appUser;
            reservationOrder.BrandId = paymentView.BrandId;
            reservationOrder.ModelId = paymentView.ModelId;
            reservationOrder.CityId = paymentView.CityId;       
            reservationOrder.PhoneNumber = paymentView.PhoneNumber;     
            reservationOrder.Dropoff=paymentView.Dropoff;       
            reservationOrder.PickUp=paymentView.PickUp;
            reservationOrder.Brand = paymentView.Brand;
            reservationOrder.Model = paymentView.Model;
            reservationOrder.City = paymentView.City;
            paymentView.Cities = _context.Cities.Where(x => !x.IsDeleted).ToList();
            paymentView.BrandId = car.BrandId;
            paymentView.Brand = car.Brand;
            paymentView.ModelId = car.ModelId;
            paymentView.Model = car.Model;
            paymentView.Cities = _context.Cities.Where(x => !x.IsDeleted).ToList();
            paymentView.AppUser = appUser;
            paymentView.AppUserId = appUser.Id;
            paymentView.AppUser.Name = appUser.Name;
            paymentView.AppUser.SurName = appUser.SurName;



            _context.Add(reservationOrder);
            _context.SaveChanges();
            TempData["odenis"] = "Odenis etmek istediyinze eminsiz";
            return View(paymentView);
           
        }
        public async Task<IActionResult> PlaceOrder()
        {
            var client = new RestClient("https://api.payriff.com/api/v2/createOrder");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "894D14D6CBE44397AC28DFF6C5BE2A43");
            var body = @"{" + "\n" +
            @"    ""body"": {" + "\n" +
            @$"        ""amount"": 50," + "\n" +
            @"        ""currencyType"": ""AZN""," + "\n" +
            @"        ""description"": ""Example""," + "\n" +
            @"        ""language"": ""AZ""," + "\n" +
            @$"        ""approveURL"": "" ""," + "\n" +
            @"        ""cancelURL"": "" ""," + "\n" +
            @"        ""declineURL"": "" """ + "\n" +
            @"    }," + "\n" +
            @"    ""merchant"": ""ES1091685""" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Post(request);
            var responseJson = JsonConvert.DeserializeObject<dynamic>(response.Content);
            var paymentUrl = responseJson.payload.paymentUrl;

            return Json(new CommandJsonResponse(false, $"{paymentUrl}"));

        }

       
    }
}

using FinalBackend.Context;
using FinalBackend.Models;
using FinalBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalBackend.Controllers
{
    public class CommentController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RentDbContext _context;
        private readonly IMailService _MailService;
        public CommentController(UserManager<AppUser> userManager,RentDbContext context,IMailService mailservice )
        {
            _userManager= userManager;  
            _context= context;
            _MailService = mailservice;
        }

        [HttpPost]
      public async Task<IActionResult> AddComment(Comment? comment)
        {


            
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                comment.AppUserId = appUser.Id;
            
         
          


          
            comment.CreatedTime= DateTime.Now;  
            _context.Add(comment);
            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async  Task<IActionResult> Delete(int id)
        {
            Comment comment = _context.Comments.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            comment.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("detail","car");
        }

        public async Task<IActionResult> Subscribe(string email)
        {
            Subscribe subscribe = new Subscribe();


            subscribe.Email = email;
            _context.Add(subscribe);
            _context.SaveChanges();



            TempData["Subscribe"] = "Ugurla Abone olduz";
           

            return Redirect(Request.Headers["Referer"].ToString());



        }
    }
}

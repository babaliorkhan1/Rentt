using FinalBackend.Extensions;
using FinalBackend.Models;
using FinalBackend.Services;
using FinalBackend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalBackend.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMailService _MailService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env,IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env; 
            _MailService= mailService;      
        }


        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Register(RegsiterViewModel regsiterViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = new AppUser
            {
                Name = regsiterViewModel.Name,
                Email = regsiterViewModel.Email,
                SurName = regsiterViewModel.SurName,
                UserName = regsiterViewModel.UserName,
            
            };
            if (regsiterViewModel.FormFile!=null)
            {
                appUser.Image = regsiterViewModel.FormFile.CreateImage(_env.WebRootPath,"assets/Images");
            }
           
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, regsiterViewModel.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(regsiterViewModel);

            }
            await _userManager.AddToRoleAsync(appUser, "User");


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var link = Url.Action(action: "VerifyEmail", controller: "account", values: new { token = token, email = appUser.Email }, protocol: Request.Scheme);


            string text = $"<a href='{link}' id='link' target='_blank' style='display:inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Click me for verify email</a>";


            await _MailService.Send("pm2459179@gmail.com", appUser.Email, link, text, "Verify Email");

            TempData["Register"] = "Please Verify your Email";


            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Info()
        {
            string userName = User.Identity.Name;
            AppUser appUser = await _userManager.FindByNameAsync(userName);
            return View(appUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AppUser appUser = await _userManager.FindByNameAsync(login.UserName);
            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName or Paswword is inCorrect");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(appUser,login.Password,login.IsRememberMe,true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is blocked for 5 minutes");
                    return View();
                }
                ModelState.AddModelError("", "UserName or Paswword is inCorrect");
                return View();
            }
            return RedirectToAction("index", "home");

        }



        [Authorize]
        public async Task<IActionResult> Update()
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (AppUser == null)
            {
                return NotFound();
            }
            UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel
            {
                Name = AppUser.Name,
                SurName = AppUser.SurName,
                Email = AppUser.Email,
                UserName = AppUser.UserName

            };
            return View(userUpdateViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel model)
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (AppUser == null)
            {
                return NotFound();
            }
            AppUser.Name = model.Name;
            AppUser.SurName = model.SurName;
            AppUser.UserName = model.UserName;
            AppUser.Email = model.Email;


            var result = await _userManager.UpdateAsync(AppUser);//paroldan basqa her seyi update edir 
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);

            }

            if (!string.IsNullOrWhiteSpace(model.newPassword))
            {
                result = await _userManager.ChangePasswordAsync(AppUser, model.CurrentPassword, model.newPassword);



                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
            }
            await _signInManager.SignInAsync(AppUser, true);
            TempData["updatedUser"] = "Sistemde duzelisler edildi";
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var link = Url.Action(action: "resetpassword", controller: "account", values: new { token = token, email = user.Email }, protocol: Request.Scheme);

            string text = $"<a href='{link}' id='link' target='_blank' style='display:inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Click me for Reset Passwored</a>";


            await _MailService.Send("pm2459179@gmail.com", user.Email, link, text, "Reset pAswword");


            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            ResetPasswordViewModel passwordViewModel = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };
            return View(passwordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid) ///bunun sebebi nedirki modelstate vermiyende reset ede bilmir elaqesi olamdigin dusunurem
            {
                return View(resetPassword);
            }
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded)
            {
                return Json(result.Errors);
            }
            return RedirectToAction("login", "account");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

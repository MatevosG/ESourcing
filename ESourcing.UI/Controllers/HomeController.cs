using ESourcing.Core.Entities;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Login(LoginViewModel loginModel)
        {
            return View();
        }
        public IActionResult Signup( )
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(AppUserViewModel signupModel)
        {
            if (ModelState.IsValid)
            {
                AppUser usr = new AppUser();
                usr.FirstName = signupModel.FirstName;
                usr.Email = signupModel.Email;
                usr.LastName = signupModel.LastName;
                usr.PhoneNumber = signupModel.PhoneNumber;
                usr.UserName = signupModel.UserName;
                if (signupModel.UserSelectTypeId == 1)
                {
                    usr.IsBuyer = true;
                    usr.IsSeller = false;
                }
                else
                {
                    usr.IsSeller = true;
                    usr.IsBuyer = false;
                }

                var result = await _userManager.CreateAsync(usr, signupModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(signupModel);
        }
    }
}
 
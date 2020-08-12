using System.Linq;
using System.Threading.Tasks;
using Application.TemporaryModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace TrasyRowerowe.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogIn postedUser)
        {
            var url = Request.Headers["Referer"].ToString();
            var start = url.IndexOf("ReturnUrl=") + "ReturnUrl=".Count();
            var returnUrl = url.Substring(start);
            var route = returnUrl.Replace("%2F","/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(postedUser.Email);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, postedUser.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (Url.IsLocalUrl($@"{route}"))
                        {
                            return Redirect(route);
                        }

                        return View();
                    }

                    ModelState.AddModelError(nameof(postedUser.Email),
                    "Nieprawidłowa nazwa użytkownika lub hasło.");
                }
            }
            return View(postedUser);
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateUser model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Name
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { information = "Udało się wylogować" });
        }
    }
}
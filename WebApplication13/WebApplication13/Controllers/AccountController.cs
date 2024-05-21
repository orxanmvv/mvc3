using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication13.Models;
using WebApplication13.ViewModels.Account;

namespace WebApplication13.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid) return View();
            User user1 = new User()
            {

                UserName = registerVm.Name,

                Email = registerVm.Email,
                Name = registerVm.Name,
                Surname = registerVm.Surname,
            };

            var result = await _userManager.CreateAsync(user1, registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);


                }
                return View();
            }



            return RedirectToAction("Index");


        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm, string? Returnurl = null)
        {
            if (ModelState.IsValid) { return View(); }

            User user2;
            if (loginVm.UsernameOrEmail.Contains("@"))
            {
                user2 = await _userManager.FindByEmailAsync(loginVm.UsernameOrEmail);
            }
            else
            {
                user2 = await _userManager.FindByNameAsync(loginVm.UsernameOrEmail);
            }
            if (user2 == null)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sefdir");
                return View();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user2, loginVm.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden cehd edin");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UsernameOrEmail ve ya Password sefdir");
                return View();
            }
            await _signInManager.SignInAsync(user2,loginVm.RememberMe);

            if (Returnurl != null)
            {
                return Redirect(Returnurl);
            }
            return RedirectToAction("Index", "Home");

        }

    }
}

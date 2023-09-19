using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,


                };


                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    // assign the user the "User" role

                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "user");

                    if (roleIdentityResult.Succeeded)
                    {
                        // show succdess notif
                        return RedirectToAction("Register");
                    }


                }
            }




            // show error notif

            return View();
        }



        [HttpGet]

        public async Task<IActionResult> LogIn(string ReturnUrl)
        {
            var model = new LogInViewModel
            {
                ReturnUrl = ReturnUrl
            };



            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {

            if (ModelState.IsValid)
            {
                return View();
            }
          var signInResult =  await signInManager.PasswordSignInAsync(logInViewModel.Username, logInViewModel.Password,false,false);

            if(signInResult != null && signInResult.Succeeded)

                if (!string.IsNullOrWhiteSpace(logInViewModel.ReturnUrl))
                {
                    return Redirect(logInViewModel.ReturnUrl);

                }
            {
                return RedirectToAction("Index", "home");
            }

            // show error

            return View();
        }



        [HttpGet]

        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();


            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}

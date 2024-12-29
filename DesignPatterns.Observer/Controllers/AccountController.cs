using BaseProject.Controllers;
using DesignPatterns.Observer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatterns.Observer.Controllers;

public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return View();

        var result = await signInManager.PasswordSignInAsync(user, password, true, false);

        if (!result.Succeeded)
        {
            return View();
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");

    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");

    }

    public async Task<IActionResult> SignUp(UserCreateViewModel userCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userCreateViewModel);
        }

        var appUser = new AppUser
        {
            UserName = userCreateViewModel.UserName,
            Email = userCreateViewModel.Email
        };

        if (string.IsNullOrWhiteSpace(userCreateViewModel.Password))
        {
            ModelState.AddModelError("Password", "Password cannot be null or empty.");
            return View(userCreateViewModel);
        }

        var identityResult = await userManager.CreateAsync(appUser, userCreateViewModel.Password);

        if (identityResult.Succeeded)
        {
            ViewBag.message = "Üyelik işlemi başarıyla gerçekleşti.";
        }
        else
        {
            ViewBag.message = identityResult.Errors.FirstOrDefault()?.Description;
        }

        return View();

    }
}

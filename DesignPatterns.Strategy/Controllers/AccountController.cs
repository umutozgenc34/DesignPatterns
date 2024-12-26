using DesignPatterns.Strategy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers;

public class AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email,string password)
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
}

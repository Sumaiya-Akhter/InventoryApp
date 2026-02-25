using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InventoryApp.Models;

namespace InventoryApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View();
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");
        ModelState.AddModelError("", "Invalid login attempt");
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalLoginCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null) return RedirectToAction("Login");

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email)!;
        var user = new IdentityUser { UserName = email, Email = email };
        await _userManager.CreateAsync(user);
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, false);
        return RedirectToAction("Index", "Home");
    }
}
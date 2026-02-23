using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var userRoles = new Dictionary<string, bool>();
        foreach (var user in users)
            userRoles[user.Id] = await _userManager.IsInRoleAsync(user, "Admin");
        ViewBag.UserRoles = userRoles;
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> ToggleAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
        if (await _userManager.IsInRoleAsync(user, "Admin"))
            await _userManager.RemoveFromRoleAsync(user, "Admin");
        else
            await _userManager.AddToRoleAsync(user, "Admin");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ToggleBlock(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
        user.LockoutEnabled = true;
        if (user.LockoutEnd == null || user.LockoutEnd < DateTime.UtcNow)
            user.LockoutEnd = DateTime.UtcNow.AddYears(100);
        else
            user.LockoutEnd = null;
        await _userManager.UpdateAsync(user);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();
        await _userManager.DeleteAsync(user);
        return RedirectToAction("Index");
    }
}
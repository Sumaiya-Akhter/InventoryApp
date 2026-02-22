using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Controllers;

public class InventoryController : Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public InventoryController(AppDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var inventories = await _db.Inventories.ToListAsync();
        return View(inventories);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Inventory inventory)
    {
        inventory.CreatorId = _userManager.GetUserId(User)!;
        inventory.CreatedAt = DateTime.UtcNow;
        _db.Inventories.Add(inventory);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
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
        var inventories = await _db.Inventories
            .Include(i => i.Creator)
            .ToListAsync();
        return View(inventories);
    }
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]

    [HttpPost]
    public async Task<IActionResult> Create(Inventory inventory)
    {
        inventory.CreatorId = _userManager.GetUserId(User)!;
        inventory.CreatedAt = DateTime.UtcNow;
        _db.Inventories.Add(inventory);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Details(int id)
    {
        var inventory = await _db.Inventories
            .Include(i => i.Creator)
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (inventory == null)
            return NotFound();

        return View(inventory);
    }
    [Authorize]
    public IActionResult AddItem(int inventoryId)
    {
        var item = new Item { InventoryId = inventoryId };
        return View(item);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddItem(Item item)
    {
        item.CreatorId = _userManager.GetUserId(User)!;
        item.CreatedAt = DateTime.UtcNow;
        _db.Items.Add(item);
        await _db.SaveChangesAsync();
        return RedirectToAction("Details", new { id = item.InventoryId });
    }
}
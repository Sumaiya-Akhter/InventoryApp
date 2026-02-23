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
    public async Task<IActionResult> AddItem(int inventoryId)
    {
        var inventory = await _db.Inventories.FindAsync(inventoryId);
        if (inventory == null) return NotFound();
        ViewBag.Inventory = inventory;
        return View(new Item { InventoryId = inventoryId });
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
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DeleteItems(int inventoryId, int[] selectedIds)
    {
        var items = await _db.Items
            .Where(i => selectedIds.Contains(i.Id) && i.InventoryId == inventoryId)
            .ToListAsync();
        _db.Items.RemoveRange(items);
        await _db.SaveChangesAsync();
        return RedirectToAction("Details", new { id = inventoryId });
    }
}
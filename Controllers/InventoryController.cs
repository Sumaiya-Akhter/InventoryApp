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

        var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        ViewBag.CurrentUserId = currentUserId;
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
    [Authorize]
public async Task<IActionResult> Edit(int id)
{
    var inventory = await _db.Inventories.FindAsync(id);
    if (inventory == null) return NotFound();
    if (inventory.CreatorId != _userManager.GetUserId(User))
        return Forbid();
    return View(inventory);
}

[Authorize]
[HttpPost]
public async Task<IActionResult> Edit(Inventory inventory)
{
    var existing = await _db.Inventories.FindAsync(inventory.Id);
    if (existing == null) return NotFound();
    if (existing.CreatorId != _userManager.GetUserId(User))
        return Forbid();

    existing.Title = inventory.Title;
    existing.Description = inventory.Description;
    existing.String1Name = inventory.String1Name;
    existing.String2Name = inventory.String2Name;
    existing.String3Name = inventory.String3Name;
    existing.Int1Name = inventory.Int1Name;
    existing.Int2Name = inventory.Int2Name;
    existing.Int3Name = inventory.Int3Name;
    existing.Bool1Name = inventory.Bool1Name;
    existing.Bool2Name = inventory.Bool2Name;
    existing.Bool3Name = inventory.Bool3Name;

    await _db.SaveChangesAsync();
    return RedirectToAction("Details", new { id = inventory.Id });
}
}
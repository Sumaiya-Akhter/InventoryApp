using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var latest = await _db.Inventories
            .Include(i => i.Creator)
            .OrderByDescending(i => i.CreatedAt)
            .Take(10)
            .ToListAsync();

        var popular = await _db.Inventories
            .Include(i => i.Creator)
            .OrderByDescending(i => i.Items.Count)
            .Take(5)
            .ToListAsync();

        var tags = await _db.Tags.ToListAsync();

        ViewBag.Latest = latest;
        ViewBag.Popular = popular;
        ViewBag.Tags = tags;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

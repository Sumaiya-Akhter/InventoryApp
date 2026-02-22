using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Inventory> Inventories { get; set; }
}
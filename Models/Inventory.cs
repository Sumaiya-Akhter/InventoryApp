using Microsoft.AspNetCore.Identity;

namespace InventoryApp.Models;

public class Inventory
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatorId { get; set; } = "";
    public IdentityUser? Creator { get; set; }
    public List<Item> Items { get; set; } = new();
}
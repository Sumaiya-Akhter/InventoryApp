using Microsoft.AspNetCore.Identity;

namespace InventoryApp.Models;

public class Item
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public Inventory? Inventory { get; set; }
    public string CreatorId { get; set; } = "";
    public IdentityUser? Creator { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Custom fields
    public string? String1 { get; set; }
    public string? String2 { get; set; }
    public string? String3 { get; set; }
    public int? Int1 { get; set; }
    public int? Int2 { get; set; }
    public int? Int3 { get; set; }
    public bool Bool1 { get; set; }
    public bool Bool2 { get; set; }
    public bool Bool3 { get; set; }
}
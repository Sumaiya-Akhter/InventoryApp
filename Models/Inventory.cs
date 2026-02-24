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
    public List<Comment> Comments { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public string? String1Name { get; set; }
    public string? String2Name { get; set; }
    public string? String3Name { get; set; }
    public string? Int1Name { get; set; }
    public string? Int2Name { get; set; }
    public string? Int3Name { get; set; }
    public string? Bool1Name { get; set; }
    public string? Bool2Name { get; set; }
    public string? Bool3Name { get; set; }
}
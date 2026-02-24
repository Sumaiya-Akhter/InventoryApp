using Microsoft.AspNetCore.Identity;

namespace InventoryApp.Models;

public class Comment
{
    public int Id { get; set; }
    public int InventoryId { get; set; }
    public Inventory? Inventory { get; set; }
    public string CreatorId { get; set; } = "";
    public IdentityUser? Creator { get; set; }
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
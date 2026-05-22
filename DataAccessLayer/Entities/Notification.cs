using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Notification
{
    public int Id { get; set; }
    [Required, MaxLength(500)]
    public string Message { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Type { get; set; } = "Info";
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User? User { get; set; }
}

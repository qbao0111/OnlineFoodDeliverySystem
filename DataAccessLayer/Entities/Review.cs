using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Review
{
    public int Id { get; set; }
    [Range(1, 5)]
    public int Rating { get; set; }
    [MaxLength(500)]
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Restaurant
{
    public int Id { get; set; }

    [Required, MaxLength(140)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(250)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(30)]
    public string Status { get; set; } = "Active";

    public int RestaurantOwnerId { get; set; }
    public RestaurantOwner? RestaurantOwner { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Customer : User
{
    [MaxLength(250)]
    public string Address { get; set; } = string.Empty;

    public Cart? Cart { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

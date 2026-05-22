using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Driver : User
{
    [MaxLength(30)]
    public string VehicleNumber { get; set; } = string.Empty;

    public bool IsAvailable { get; set; } = true;
    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}

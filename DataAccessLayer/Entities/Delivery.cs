namespace DataAccessLayer.Entities;

public class Delivery
{
    public int Id { get; set; }
    public DateTime? PickupTime { get; set; }
    public DateTime? DeliveredTime { get; set; }
    public string DeliveryStatus { get; set; } = "WaitingForDriver";
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int? DriverId { get; set; }
    public Driver? Driver { get; set; }
}

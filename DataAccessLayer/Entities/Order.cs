using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "PendingPayment";

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public Payment? Payment { get; set; }
    public Delivery? Delivery { get; set; }
}

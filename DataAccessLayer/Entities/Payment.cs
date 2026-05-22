using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Payment
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [MaxLength(40)]
    public string PaymentMethod { get; set; } = string.Empty;

    [MaxLength(40)]
    public string PaymentStatus { get; set; } = "Pending";

    public DateTime? PaidAt { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
}

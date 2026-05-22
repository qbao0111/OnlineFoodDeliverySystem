using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
}

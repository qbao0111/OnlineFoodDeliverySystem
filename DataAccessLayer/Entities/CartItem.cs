using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public int CartId { get; set; }
    public Cart? Cart { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
}

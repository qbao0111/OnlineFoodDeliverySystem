namespace DataAccessLayer.Entities;

public class RestaurantOwner : User
{
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}

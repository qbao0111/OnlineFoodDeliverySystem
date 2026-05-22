namespace BusinessLayer.DTOs;

public record RestaurantDto(int Id, string Name, string Address, string PhoneNumber, string Status);
public record CreateRestaurantDto(string Name, string Address, string PhoneNumber, int RestaurantOwnerId);
public record MenuItemDto(int Id, int RestaurantId, string Name, string Description, decimal Price, bool IsAvailable);
public record UpsertMenuItemDto(int RestaurantId, string Name, string Description, decimal Price, bool IsAvailable);

using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menu;

    public MenuService(IMenuRepository menu)
    {
        _menu = menu;
    }

    public async Task<IReadOnlyList<MenuItemDto>> GetMenuAsync(int restaurantId, CancellationToken cancellationToken = default) =>
        (await _menu.GetByRestaurantAsync(restaurantId, cancellationToken)).Select(ToDto).ToList();

    public async Task<MenuItemDto> CreateMenuItemAsync(UpsertMenuItemDto request, CancellationToken cancellationToken = default)
    {
        var item = new MenuItem
        {
            RestaurantId = request.RestaurantId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            IsAvailable = request.IsAvailable
        };
        await _menu.AddAsync(item, cancellationToken);
        await _menu.SaveChangesAsync(cancellationToken);
        return ToDto(item);
    }

    public async Task<bool> DeleteMenuItemAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _menu.GetByIdAsync(id, cancellationToken);
        if (item is null) return false;
        _menu.Delete(item);
        await _menu.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static MenuItemDto ToDto(MenuItem item) =>
        new(item.Id, item.RestaurantId, item.Name, item.Description, item.Price, item.IsAvailable);
}

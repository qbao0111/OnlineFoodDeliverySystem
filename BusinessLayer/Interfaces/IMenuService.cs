using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IMenuService
{
    Task<IReadOnlyList<MenuItemDto>> GetMenuAsync(int restaurantId, CancellationToken cancellationToken = default);
    Task<MenuItemDto> CreateMenuItemAsync(UpsertMenuItemDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteMenuItemAsync(int id, CancellationToken cancellationToken = default);
}

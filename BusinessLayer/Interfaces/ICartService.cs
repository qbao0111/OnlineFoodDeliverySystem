using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface ICartService
{
    Task<ApiResponse<CartDto>> GetCartAsync(int customerId, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> AddItemAsync(AddCartItemDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<CartDto>> RemoveItemAsync(int customerId, int cartItemId, CancellationToken cancellationToken = default);
}

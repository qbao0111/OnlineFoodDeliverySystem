using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces;

public interface IReviewService
{
    Task<ApiResponse<ReviewDto>> AddReviewAsync(CreateReviewDto request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReviewDto>> GetReviewsAsync(int restaurantId, CancellationToken cancellationToken = default);
}

using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviews;

    public ReviewService(IReviewRepository reviews)
    {
        _reviews = reviews;
    }

    public async Task<ApiResponse<ReviewDto>> AddReviewAsync(CreateReviewDto request, CancellationToken cancellationToken = default)
    {
        if (request.Rating is < 1 or > 5) return ApiResponse<ReviewDto>.Fail("Rating must be between 1 and 5.");

        var review = new Review { CustomerId = request.CustomerId, RestaurantId = request.RestaurantId, Rating = request.Rating, Comment = request.Comment };
        await _reviews.AddAsync(review, cancellationToken);
        await _reviews.SaveChangesAsync(cancellationToken);
        return ApiResponse<ReviewDto>.Ok(ToDto(review), "Review submitted.");
    }

    public async Task<IReadOnlyList<ReviewDto>> GetReviewsAsync(int restaurantId, CancellationToken cancellationToken = default) =>
        (await _reviews.GetByRestaurantAsync(restaurantId, cancellationToken)).Select(ToDto).ToList();

    private static ReviewDto ToDto(Review review) =>
        new(review.Id, review.CustomerId, review.RestaurantId, review.Rating, review.Comment, review.CreatedAt);
}

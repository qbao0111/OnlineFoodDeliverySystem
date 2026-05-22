namespace BusinessLayer.DTOs;

public record CreateReviewDto(int CustomerId, int RestaurantId, int Rating, string Comment);
public record ReviewDto(int Id, int CustomerId, int RestaurantId, int Rating, string Comment, DateTime CreatedAt);

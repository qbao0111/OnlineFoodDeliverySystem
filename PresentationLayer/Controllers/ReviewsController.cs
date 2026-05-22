using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviews;

    public ReviewsController(IReviewService reviews)
    {
        _reviews = reviews;
    }

    [HttpGet("restaurants/{restaurantId:int}")]
    public async Task<IActionResult> GetReviews(int restaurantId, CancellationToken cancellationToken) =>
        Ok(await _reviews.GetReviewsAsync(restaurantId, cancellationToken));

    [Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> AddReview(CreateReviewDto request, CancellationToken cancellationToken)
    {
        var result = await _reviews.AddReviewAsync(request, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

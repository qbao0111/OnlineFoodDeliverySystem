using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurants;

    public RestaurantsController(IRestaurantService restaurants)
    {
        _restaurants = restaurants;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurants(CancellationToken cancellationToken) =>
        Ok(await _restaurants.BrowseRestaurantsAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurant(int id, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurants.GetRestaurantAsync(id, cancellationToken);
        return restaurant is null ? NotFound() : Ok(restaurant);
    }

    [Authorize(Roles = "Admin,Restaurant")]
    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurants.CreateRestaurantAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
    }
}

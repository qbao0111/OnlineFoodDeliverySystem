using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminController(IAdminService admin)
    {
        _admin = admin;
    }

    [HttpGet("users")]
    public async Task<IActionResult> ManageUsers(CancellationToken cancellationToken) =>
        Ok(await _admin.ManageUsersAsync(cancellationToken));

    [HttpGet("reports")]
    public async Task<IActionResult> ViewReports(CancellationToken cancellationToken) =>
        Ok(await _admin.ViewReportsAsync(cancellationToken));
}

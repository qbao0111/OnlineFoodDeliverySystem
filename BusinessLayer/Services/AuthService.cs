using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using BusinessLayer.Validators;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository users, IConfiguration configuration)
    {
        _users = users;
        _configuration = configuration;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
    {
        var validation = RequestValidators.Validate(request);
        if (validation is not null) return ApiResponse<AuthResponseDto>.Fail(validation);
        if (await _users.GetByEmailAsync(request.Email, cancellationToken) is not null) return ApiResponse<AuthResponseDto>.Fail("Email is already registered.");

        User user = request.Role.ToLowerInvariant() switch
        {
            "restaurant" or "restaurantowner" => new RestaurantOwner(),
            "driver" => new Driver(),
            "admin" => new Admin(),
            _ => new Customer { Address = request.Address ?? string.Empty }
        };

        user.FullName = request.FullName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Role = NormalizeRole(request.Role);
        user.PasswordHash = HashPassword(request.Password);

        await _users.AddAsync(user, cancellationToken);
        await _users.SaveChangesAsync(cancellationToken);

        return ApiResponse<AuthResponseDto>.Ok(ToAuthResponse(user), "Registered successfully.");
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await _users.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null || user.PasswordHash != HashPassword(request.Password)) return ApiResponse<AuthResponseDto>.Fail("Invalid email or password.");
        return ApiResponse<AuthResponseDto>.Ok(ToAuthResponse(user), "Logged in successfully.");
    }

    private AuthResponseDto ToAuthResponse(User user) => new(user.Id, user.FullName, user.Email, user.Role, GenerateToken(user));

    private string GenerateToken(User user)
    {
        var jwt = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"] ?? "development-secret-key-change-me-please"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    private static string NormalizeRole(string role) => role.ToLowerInvariant() switch
    {
        "restaurant" or "restaurantowner" => "Restaurant",
        "driver" => "Driver",
        "admin" => "Admin",
        _ => "Customer"
    };
}

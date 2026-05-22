namespace BusinessLayer.DTOs;

public record RegisterRequestDto(string FullName, string Email, string Password, string PhoneNumber, string Role, string? Address);
public record LoginRequestDto(string Email, string Password);
public record AuthResponseDto(int UserId, string FullName, string Email, string Role, string Token);
public record UserDto(int Id, string FullName, string Email, string PhoneNumber, string Role);

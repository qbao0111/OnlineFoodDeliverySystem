using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public abstract class User
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Role { get; set; } = string.Empty;

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

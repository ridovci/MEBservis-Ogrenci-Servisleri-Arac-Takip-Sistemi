using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Kullanıcı bilgilerini temsil eden sınıf.
/// </summary>
public class User
{
    /// <summary>
    /// Benzersiz ID.
    /// </summary>
    [Key]
    public int UserId { get; set; }

    /// <summary>
    /// Kullanıcının adı.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının soyadı.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının TC Kimlik Numarası.
    /// </summary>
    [Required]
    [MaxLength(11)]
    public string TcKimlikNo { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının şifresi (hashlenmiş).
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının telefon numarası.
    /// </summary>
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Kullanıcının araç plaka numarası.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string VehiclePlateNo { get; set; } = string.Empty;

    /// <summary>
    /// Hesabın oluşturulma tarihi.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Kullanıcının rolü (varsayılan olarak 1).
    /// </summary>
    [Required]
    public int RoleId { get; set; } = 1;

    /// <summary>
    /// Kullanıcının sahip olduğu takip oturumları.
    /// </summary>
    public ICollection<TrackingSession> TrackingSessions { get; set; } = new List<TrackingSession>();

    /// <summary>
    /// Kullanıcının rolü.
    /// </summary>
    [ForeignKey("RoleId")]
    public UserRole? UserRole { get; set; }
}
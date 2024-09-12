using System.ComponentModel.DataAnnotations;


/// <summary>
/// Kullanıcı rollerini temsil eden sınıf.
/// </summary>
public class UserRole
{
    /// <summary>
    /// Benzersiz kullanıcı rolü ID'si.
    /// </summary>
    [Key]
    public int UserRoleId { get; set; }

    /// <summary>
    /// Kullanıcı rolünün adı.
    /// </summary>
    [Required]
    public string UserRoleName { get; set; } = string.Empty;
}


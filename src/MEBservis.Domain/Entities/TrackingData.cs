using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Konum verilerini takip etmek için kullanılan sınıf.
/// </summary>
public class TrackingData
{
    /// <summary>
    /// Otomatik artan benzersiz ID.
    /// </summary>
    [Key]
    public int TrackingId { get; set; }

    /// <summary>
    /// İlgili oturum ID'si.
    /// </summary>
    [Required]
    public int SessionId { get; set; }

    /// <summary>
    /// Takip oturumu entity'si ile ilişki.
    /// </summary>
    [ForeignKey("SessionId")]
    public TrackingSession TrackingSession { get; set; }

    /// <summary>
    /// Enlem bilgisi.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Latitude { get; set; }

    /// <summary>
    /// Boylam bilgisi.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Longitude { get; set; }

    /// <summary>
    /// Konumun kaydedildiği tarih ve saat.
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

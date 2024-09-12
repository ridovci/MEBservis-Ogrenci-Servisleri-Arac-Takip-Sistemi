using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
    /// Kullanıcı takip oturumlarını temsil eden sınıf.
    /// </summary>
public class TrackingSession
{
    /// <summary>
    /// Otomatik artan benzersiz ID.
    /// </summary>
    [Key]
    public int SessionId { get; set; }

    /// <summary>
    /// İlgili kullanıcı ID'si.
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// Kullanıcı entity'si ile ilişki.
    /// </summary>
    [ForeignKey("UserId")]
    public User User { get; set; }

    /// <summary>
    /// Takibin başlatıldığı zaman.
    /// </summary>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Takibin bitirildiği zaman (nullable).
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Takip oturumuna ait konum verileri.
    /// </summary>
    public ICollection<TrackingData> TrackingData { get; set; } = new List<TrackingData>();
}


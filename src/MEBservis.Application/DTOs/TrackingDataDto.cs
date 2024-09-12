using System.ComponentModel.DataAnnotations;

namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı tarafından gönderilen konum verilerini temsil eden DTO sınıfı.
    /// </summary>
    public class TrackingDataDto
    {
        /// <summary>
        /// Kullanıcının benzersiz ID'si.
        /// </summary>
        [Required(ErrorMessage = "Kullanıcı ID'si gereklidir.")]
        public int UserId { get; set; }

        /// <summary>
        /// Enlem bilgisi.
        /// </summary>
        [Required(ErrorMessage = "Enlem gereklidir.")]
        [MaxLength(50)]
        public string Latitude { get; set; }

        /// <summary>
        /// Boylam bilgisi.
        /// </summary>
        [Required(ErrorMessage = "Boylam gereklidir.")]
        [MaxLength(50)]
        public string Longitude { get; set; }

        /// <summary>
        /// Konumun kaydedildiği tarih ve saat.
        /// </summary>
        [Required(ErrorMessage = "Tarih-saat gereklidir.")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

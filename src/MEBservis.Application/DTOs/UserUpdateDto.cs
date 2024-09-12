using System.ComponentModel.DataAnnotations;

namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı güncelleme bilgilerini içeren veri transfer nesnesi (DTO).
    /// </summary>
    public class UserUpdateDto
    {
        /// <summary>
        /// Güncellenmek istenen kullanıcının benzersiz kimlik numarası.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcının telefon numarası.
        /// Maksimum uzunluk 15 karakterdir.
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının araç plaka numarası.
        /// Maksimum uzunluk 20 karakterdir.
        /// </summary>
        [MaxLength(20)]
        public string? VehiclePlateNo { get; set; } = string.Empty;

    }
}

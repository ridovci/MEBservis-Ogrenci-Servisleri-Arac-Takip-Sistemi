using System.ComponentModel.DataAnnotations;

namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı oluşturma işlemi için gerekli bilgileri içeren veri transfer nesnesi (DTO).
    /// </summary>
    public class UserCreateDto
    {
        /// <summary>
        /// Kullanıcının adı. Bu alan zorunludur ve maksimum 100 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının soyadı. Bu alan zorunludur ve maksimum 100 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının T.C. Kimlik Numarası. Bu alan zorunludur ve maksimum 11 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        [MaxLength(11)]
        public string TcKimlikNo { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının şifresi. Bu alan zorunludur.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının telefon numarası. Bu alan zorunludur ve maksimum 15 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının araç plaka numarası. Bu alan zorunludur ve maksimum 20 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string VehiclePlateNo { get; set; } = string.Empty;
    }
}

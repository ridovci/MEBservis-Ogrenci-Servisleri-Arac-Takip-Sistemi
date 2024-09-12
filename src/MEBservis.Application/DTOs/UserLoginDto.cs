using System.ComponentModel.DataAnnotations;

namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı giriş bilgilerini içeren veri transfer nesnesi (DTO).
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Kullanıcının T.C. Kimlik Numarası. Bu alan zorunludur ve maksimum 11 karakter uzunluğunda olabilir.
        /// </summary>
        [Required]
        public string TcKimlikNo { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının şifresi. Bu alan zorunludur.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }

}

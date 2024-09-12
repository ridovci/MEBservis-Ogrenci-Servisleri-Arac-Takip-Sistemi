using System.ComponentModel.DataAnnotations;

namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı yeni rol bilgisini içeren veri transfer nesnesi (DTO).
    /// </summary>
    public class UserRoleUpdateDto
    {
        /// <summary>
        /// Kullanıcı Id'si
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcı yeni rolü
        /// </summary>
        [Required]
        public int NewRoleId { get; set; }
    }

}

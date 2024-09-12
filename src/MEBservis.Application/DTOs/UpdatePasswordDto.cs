namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Şifre güncelleme işlemi için kullanılan veri transfer nesnesi (DTO).
    /// </summary>
    public class UpdatePasswordDto
    {
        /// <summary>
        /// Kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcının mevcut şifresi.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Kullanıcının yeni şifresi.
        /// </summary>
        public string NewPassword { get; set; }
    }
}

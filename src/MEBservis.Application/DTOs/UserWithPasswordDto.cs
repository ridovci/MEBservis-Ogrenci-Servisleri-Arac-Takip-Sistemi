namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Şifre bilgilerini içeren kullanıcı veri transfer nesnesi (DTO).
    /// </summary>
    public class UserWithPasswordDto
    {
        /// <summary>
        /// Kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcının şifre hash'i.
        /// </summary>
        public string PasswordHash { get; set; }
    }
}

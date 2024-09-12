namespace MEBservis.Application.DTOs
{
    /// <summary>
    /// Kullanıcı bilgilerini içeren veri transfer nesnesi (DTO).
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Kullanıcının benzersiz kimlik numarası.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Kullanıcının adı.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının soyadı.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının T.C. Kimlik Numarası.
        /// </summary>
        public string TcKimlikNo { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının telefon numarası.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının araç plaka numarası.
        /// </summary>
        public string VehiclePlateNo { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının rol ID'si.
        /// </summary>
        public int RoleId { get; set; }
    }
}

namespace MEBservis.Domain.Interfaces
{
    /// <summary>
    /// Kullanıcı veri erişim yöntemlerini tanımlar.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Veritabanındaki tüm kullanıcıları alır.
        /// </summary>
        /// <returns>Kullanıcıların bir koleksiyonu.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Veritabanında verilen ID'ye sahip kullanıcıyı bulur.
        /// </summary>
        /// <param name="id">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcı nesnesi.</returns>
        Task<User?> GetUserByIdAsync(int id);

        /// <summary>
        /// Veritabanında verilen TC Kimlik Numarasına sahip kullanıcıyı bulur.
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik Numarası.</param>
        /// <returns>Kullanıcı nesnesi.</returns>
        Task<User?> GetUserByTcKimlikNoAsync(string tcKimlikNo);

        /// <summary>
        /// Verilen TC Kimlik Numarasının veritabanında mevcut olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik Numarası.</param>
        /// <returns>TC Kimlik Numarasının var olup olmadığını belirten bir değer.</returns>
        Task<bool> TcKimlikNoExistsAsync(string tcKimlikNo);

        /// <summary>
        /// Verilen araç plaka numarasının veritabanında mevcut olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="vehiclePlateNo">Araç plaka numarası.</param>
        /// <returns>Araç plaka numarasının var olup olmadığını belirten bir değer.</returns>
        Task<bool> VehiclePlateNoExistsAsync(string vehiclePlateNo);

        /// <summary>
        /// Verilen kullanıcıyı veritabanına ekler.
        /// </summary>
        /// <param name="user">Eklenecek kullanıcı nesnesi.</param>
        /// <returns>Kullanıcının başarıyla eklenip eklenmediğini belirten bir değer.</returns>
        Task<bool> AddUserAsync(User user);

        /// <summary>
        /// Veritabanındaki mevcut bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="user">Güncellenecek kullanıcı nesnesi.</param>
        /// <returns>Kullanıcının başarıyla güncellenip güncellenmediğini belirten bir değer.</returns>
        Task<bool> UpdateUserAsync(User user);

        /// <summary>
        /// Kullanıcının admin olup olmadığını asenkron olarak kontrol eder.
        /// </summary>
        /// <param name="userId">Kontrol edilecek kullanıcının benzersiz kimliği.</param>
        /// <returns>Belirtilen kullanıcının admin olup olmadığını belirten bir <see cref="Task{Boolean}"/> döndürür.
        /// Eğer kullanıcı bulunursa ve admin rolüne sahipse, <c>true</c> döner; aksi takdirde <c>false</c> döner.</returns>
        Task<bool> IsAdminAsync(int userId);

    }
}

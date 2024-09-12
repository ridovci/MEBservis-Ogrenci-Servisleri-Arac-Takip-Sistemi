using MEBservis.Application.DTOs;

namespace MEBservis.Domain.Interfaces
{
    /// <summary>
    /// Kullanıcı ile ilgili işlemler için servis arayüzü.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Tüm kullanıcıları alır.
        /// </summary>
        /// <returns>Kullanıcıların bir koleksiyonu.</returns>
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Verilen ID'ye sahip kullanıcıyı alır.
        /// </summary>
        /// <param name="id">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcı DTO'su veya null.</returns>
        Task<UserDto?> GetUserByIdAsync(int id);

        /// <summary>
        /// Yeni bir kullanıcı ekler.
        /// </summary>
        /// <param name="userCreateDto">Eklenecek kullanıcı DTO'su.</param>
        /// <returns>Kullanıcının ID'sini döner veya eklenemezse 0.</returns>
        Task<int> AddUserAsync(UserCreateDto userCreateDto);

        /// <summary>
        /// Var olan bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="userUpdateDto">Güncellenecek kullanıcı DTO'su.</param>
        /// <returns>Kullanıcının başarıyla güncellenip güncellenmediğini belirten bir değer.</returns>
        Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto);

        /// <summary>
        /// Kullanıcının şifresini günceller.
        /// </summary>
        /// <param name="updatePasswordDto">Güncellenecek kullanıcı DTO'su.</param>
        /// <returns>Şifrenin başarıyla güncellenip güncellenmediğini belirten bir değer.</returns>
        Task<bool> UpdateUserPasswordAsync(UpdatePasswordDto updatePasswordDto);

        /// <summary>
        /// Kullanıcının admin olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcının admin olup olmadığını belirten bir değer.</returns>
        Task<bool> IsAdminAsync(int userId);
    }
}

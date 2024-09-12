using MEBservis.Application.DTOs;
using System.Threading.Tasks;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// Kullanıcı kimlik doğrulama servis arayüzü.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Kullanıcının giriş bilgilerini asenkron olarak doğrular.
        /// </summary>
        /// <param name="userLoginDto">Giriş yapmak isteyen kullanıcının giriş bilgileri.</param>
        /// <returns>JWT token veya <c>null</c>.</returns>
        Task<string?> LoginAsync(UserLoginDto userLoginDto);
    }
}

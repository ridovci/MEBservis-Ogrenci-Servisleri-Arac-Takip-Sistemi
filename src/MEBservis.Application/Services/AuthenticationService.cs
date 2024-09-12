using MEBservis.Application.DTOs;
using MEBservis.Domain.Interfaces;
using MEBservis.Application.Utilities;


namespace MEBservis.Application.Services
{
    /// <summary>
    /// Kullanıcı kimlik doğrulama işlemlerini yöneten servis sınıfı.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// AuthenticationService sınıfının bir örneğini oluşturur.
        /// </summary>
        /// <param name="userRepository">Kullanıcı repository'si.</param>
        /// <param name="tokenService">JWT token oluşturmak için kullanılan servis.</param>
        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Kullanıcının giriş bilgilerini asenkron olarak doğrular.
        /// </summary>
        /// <param name="userLoginDto">Giriş yapmak isteyen kullanıcının giriş bilgileri.</param>
        /// <returns>Giriş işleminin sonucunu belirten bir <see cref="Task{Boolean}"/> döndürür.
        /// Kullanıcı doğrulandıysa <c>true</c>, aksi takdirde <c>false</c> döner.</returns>
        public async Task<string?> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByTcKimlikNoAsync(userLoginDto.TcKimlikNo);

            if (user == null)
            {
                return null;
            }

            var isPasswordValid = PasswordHasher.VerifyPassword(userLoginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            return _tokenService.GenerateJwtToken(user);

        }

    }

}

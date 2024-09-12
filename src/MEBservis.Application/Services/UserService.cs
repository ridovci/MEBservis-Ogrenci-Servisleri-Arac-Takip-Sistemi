using AutoMapper;
using MEBservis.Application.Utilities;
using MEBservis.Application.DTOs;
using MEBservis.Domain.Interfaces;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// Kullanıcı ile ilgili işlemleri yöneten servis sınıfı.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// UserService sınıfını başlatan yapılandırıcı.
        /// </summary>
        /// <param name="userRepository">Kullanıcı repository'si.</param>
        /// <param name="mapper">AutoMapper nesnesi.</param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm kullanıcıları alır.
        /// </summary>
        /// <returns>Kullanıcıların bir koleksiyonu.</returns>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Verilen ID'ye sahip kullanıcıyı alır.
        /// </summary>
        /// <param name="id">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcı DTO'su veya null.</returns>
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        /// <summary>
        /// Yeni bir kullanıcı ekler.
        /// </summary>
        /// <param name="userCreateDto">Eklenecek kullanıcı DTO'su.</param>
        /// <param name="password">Şifreyi hashlemek için ayrı aldık.</param>
        /// <returns>Kullanıcının başarıyla eklenip eklenmediğini belirten bir değer.</returns>
        public async Task<int> AddUserAsync(UserCreateDto userCreateDto)
        {
            if (await _userRepository.TcKimlikNoExistsAsync(userCreateDto.TcKimlikNo))
            {
                return 0;
            }

            var user = _mapper.Map<User>(userCreateDto);

            // Şifreyi hashle
            var passwordHash = PasswordHasher.HashPassword(userCreateDto.PasswordHash);
            user.PasswordHash = passwordHash; // Hashlenmiş şifreyi kullanıcıya ata

            var isAdded = await _userRepository.AddUserAsync(user);

            if (!isAdded)
            {
                return 0;
            }

            return user.UserId;
        }

        /// <summary>
        /// Var olan bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="userUpdateDto">Güncellenecek kullanıcı DTO'su.</param>
        /// <returns>Kullanıcının başarıyla güncellenip güncellenmediğini belirten bir değer.</returns>
        public async Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null || userUpdateDto.UserId <= 0)
            {
                throw new ArgumentException("Geçersiz kullanıcı verisi.");
            }

            var user = _mapper.Map<User>(userUpdateDto);

            user.UserId = userUpdateDto.UserId; // UserId'yi manuel olarak ayarla
            return await _userRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Kullanıcının şifresini günceller. Eski şifre doğru ise yeni şifre hash'lenir ve kaydedilir.
        /// </summary>
        /// <param name="updatePasswordDto">Yeni şifre bilgilerini içeren DTO. DTO, kullanıcının mevcut şifresi ve yeni şifresini içerir.</param>
        /// <returns>
        /// Şifrenin başarıyla güncellenip güncellenmediğini belirten bir değer. 
        /// True dönerse güncelleme başarılıdır, False dönerse güncelleme başarısız olmuştur.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Eski veya yeni şifre boşsa veya eski şifre geçersizse bir hata fırlatılır.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Şifre doğrulama sırasında bir hata oluşursa fırlatılır.
        /// </exception>
        public async Task<bool> UpdateUserPasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            // Şifrelerin boş olup olmadığını kontrol et
            if (string.IsNullOrEmpty(updatePasswordDto.NewPassword) || string.IsNullOrEmpty(updatePasswordDto.OldPassword))
            {
                throw new ArgumentException("Eski ve yeni şifreler boş olamaz.");
            }

            // Kullanıcıyı getir
            var userDto = await GetUserWithPasswordByIdAsync(updatePasswordDto.UserId);
            if (userDto is null)
            {
                throw new ArgumentException("Kullanıcı bulunamadı.");
            }

            // Kullanıcıyı maple
            var user = _mapper.Map<User>(userDto);

            // Eski şifreyi doğrula
            try
            {
                if (!PasswordHasher.VerifyPassword(updatePasswordDto.OldPassword, user.PasswordHash))
                {
                    throw new ArgumentException("Eski şifre geçersiz.");
                }
            }
            catch (Exception ex)
            {
                // Hata hakkında daha fazla bilgi sağlar
                // Log.Error(ex, "Şifre doğrulama sırasında bir hata oluştu.");
                throw new InvalidOperationException("Şifre doğrulama sırasında bir hata oluştu.", ex);
            }

            // Yeni şifreyi hashle ve güncelle
            var newPasswordHash = PasswordHasher.HashPassword(updatePasswordDto.NewPassword);
            user.PasswordHash = newPasswordHash;

            // Kullanıcıyı güncelle
            return await _userRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Kullanıcı bilgilerini ve şifre hash'ini veritabanından alır ve DTO'ya dönüştürür.
        /// </summary>
        /// <param name="userId">Bilgileri alınacak kullanıcının benzersiz ID'si.</param>
        /// <returns>
        /// Kullanıcı bilgilerini ve şifre hash'ini içeren bir <see cref="UserWithPasswordDto"/> nesnesi döner. 
        /// Kullanıcı bulunamazsa null döner.
        /// </returns>
        public async Task<UserWithPasswordDto?> GetUserWithPasswordByIdAsync(int userId)
        {
            // Kullanıcı bilgilerini ve şifre hash'ini veritabanından al
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            // Kullanıcıyı DTO'ya AutoMapper ile dönüştür
            return _mapper.Map<UserWithPasswordDto>(user);
        }


        /// <summary>
        /// Kullanıcının admin olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcının admin olup olmadığını belirten bir değer.</returns>
        public async Task<bool> IsAdminAsync(int userId)
        {
            return await _userRepository.IsAdminAsync(userId);
        }
    }
}

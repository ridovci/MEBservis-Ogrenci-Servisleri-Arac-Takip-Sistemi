using MEBservis.Application.DTOs;
using MEBservis.Application.Services;
using MEBservis.Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MEBservis.Domain.Interfaces;

namespace MEBservis.Api.Controllers
{
    /// <summary>
    /// Kullanıcılarla ilgili HTTP isteklerini yöneten denetleyici.
    /// Kullanıcı bilgilerini almak, eklemek, güncellemek ve giriş yapmak için yöntemler sağlar.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// UserController sınıfının yapılandırıcısı.
        /// </summary>
        /// <param name="userService">Kullanıcı işlemlerini gerçekleştiren servis.</param>
        /// <param name="mapper">Entity-DTO eşletirme sağlayan servis.</param>
        /// <param name="authenticationService">Kimlik doğrulama işlemlerini gerçekleştiren servis.</param>
        public UserController(IUserService userService, IMapper mapper, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        /// <returns>HTTP 200 OK durumu ve kullanıcı listesi; eğer kullanıcı bulunamazsa HTTP 404 Not Found durumu.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(users);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip bir kullanıcıyı getirir.
        /// </summary>
        /// <param name="id">Kullanıcının ID'si.</param>
        /// <returns>HTTP 200 OK durumu ve kullanıcı bilgileri; eğer kullanıcı bulunamazsa HTTP 404 Not Found durumu.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Yeni bir kullanıcı ekler.
        /// </summary>
        /// <param name="userCreateDto">Eklenecek kullanıcının bilgileri.</param>
        /// <returns>HTTP 201 Created durumu ve yeni kullanıcı ID'si; eğer ekleme işlemi başarısızsa HTTP 400 Bad Request durumu.</returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest("Kullanıcı verisi geçersiz.");
            }

            var userId = await _userService.AddUserAsync(userCreateDto);

            if (userId <= 0)
            {
                return BadRequest("Kullanıcı eklenirken bir hata oluştu.");
            }

            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = userId }, new { id = userId });
        }

        /// <summary>
        /// Kullanıcıyı günceller.
        /// </summary>
        /// <param name="userUpdateDto">Güncellenecek kullanıcı bilgileri.</param>
        /// <returns>İşlemin sonucu hakkında bilgi verir.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null || userUpdateDto.UserId <= 0)
            {
                return BadRequest("Geçersiz kullanıcı verisi.");
            }

            var result = await _userService.UpdateUserAsync(userUpdateDto);

            if (!result)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            return Ok("Kullanıcı başarıyla güncellendi.");
        }

        /// <summary>
        /// Kullanıcı girişi sağlar. Başarılı bir giriş yapıldığında JWT token'ı döner.
        /// </summary>
        /// <param name="userLoginDto">Giriş yapmak isteyen kullanıcının bilgilerini içeren DTO. Bu DTO, kullanıcının e-posta adresi ve şifresini içerir.</param>
        /// <returns>
        /// Giriş işlemi başarılıysa, kullanıcıya ait JWT token'ını içeren bir <see cref="OkObjectResult"/> döner. 
        /// Giriş bilgileri geçersizse, <see cref="UnauthorizedResult"/> döner.
        /// Giriş verisi eksikse veya geçersizse, <see cref="BadRequestObjectResult"/> döner.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                return BadRequest("Geçersiz giriş verisi.");
            }

            var token = await _authenticationService.LoginAsync(userLoginDto);
            if (token == null)
            {
                return Unauthorized("Geçersiz e-posta veya şifre.");
            }

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Kullanıcı şifresini günceller.
        /// </summary>
        /// <param name="updatePasswordDto">Yeni şifre bilgilerini içeren DTO. DTO, kullanıcının eski şifresi ve yeni şifresini içerir.</param>
        /// <returns>
        /// Şifre güncelleme işlemi başarılıysa HTTP 200 OK durumu ve başarı mesajı döner; 
        /// güncelleme başarısızsa HTTP 400 Bad Request durumu ve başarısızlık mesajı döner; 
        /// işlem sırasında bir hata oluşursa HTTP 500 Internal Server Error durumu ve hata mesajı döner.
        /// </returns>
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                var result = await _userService.UpdateUserPasswordAsync(updatePasswordDto);
                if (result)
                {
                    return Ok("Şifre güncellendi.");
                }
                return BadRequest("Şifre güncellenemedi.");
            }
            catch (Exception ex)
            {
                // Hata hakkında bilgi verir
                var errorResponse = new
                {
                    Message = "Bir hata oluştu.",
                    Details = ex.Message // Detaylı hata mesajı
                };

                // Kullanıcıya uygun hata mesajı döndür
                return StatusCode(500, errorResponse);
            }

        }

    }
}

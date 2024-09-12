using MEBservis.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// JWT token oluşturma hizmeti sağlar.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// TokenService sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="configuration">Uygulama konfigürasyonlarını içeren nesne.</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Kullanıcı için JWT token oluşturur.
        /// </summary>
        /// <param name="user">Token oluşturulacak kullanıcı nesnesi.</param>
        /// <returns>Oluşturulan JWT token.</returns>
        public string GenerateJwtToken(User user)
        {
            // Kullanıcı için gerekli JWT claim'lerini oluşturun
            var claims = new List<Claim>
            {
                new Claim("id", user.UserId.ToString()),
                new Claim("role", user.RoleId.ToString())
            };

            // JWT imzalama anahtarını oluşturun
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);

            // JWT tokenini oluşturun
            var token = new JwtSecurityToken(
                issuer: issuer, // Tokeni oluşturan uygulamanın adresi
                claims: claims,
                expires: DateTime.Now.AddDays(7.0), // Token geçerlilik süresi
                signingCredentials: creds);

            // Token'ı string olarak döndürün
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

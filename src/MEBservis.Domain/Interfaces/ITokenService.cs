namespace MEBservis.Domain.Interfaces
{
    /// <summary>
    /// Kullanıcı kimlik doğrulama ve yetkilendirme işlemleri için JSON Web Token (JWT) oluşturma işlemlerini sağlayan arayüz.
    /// Bu arayüz, kullanıcılar için JWT token'lar oluşturmak amacıyla gerekli metodları tanımlar.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Verilen kullanıcı bilgileriyle bir JSON Web Token (JWT) oluşturur.
        /// </summary>
        /// <param name="user">Token oluşturulacak kullanıcının bilgileri.</param>
        /// <returns>
        /// Oluşturulan JWT token'ı içeren bir dize. Token, kullanıcının kimlik doğrulamasını ve yetkilendirmesini sağlamak için kullanılabilir.
        /// </returns>
        string GenerateJwtToken(User user);
    }
}
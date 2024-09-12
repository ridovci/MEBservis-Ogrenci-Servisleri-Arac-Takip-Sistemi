using BCrypt.Net;

/// <summary>
/// Şifrelerin güvenli bir biçimde hash'lenmesi ve doğrulanması işlemlerini gerçekleştiren yardımcı sınıf.
/// Bu sınıf, şifreleri hash'leyerek saklamak ve hash'lenmiş şifrelerle doğrulama yapmak için gerekli yöntemleri sağlar.
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Şifreyi hash'ler ve güvenli bir biçimde saklanabilir hale getirir.
    /// </summary>
    /// <param name="password">Hash'lenmek istenen düz metin şifre.</param>
    /// <returns>Hash'lenmiş şifre.</returns>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verilen düz metin şifrenin, hash'lenmiş şifre ile eşleşip eşleşmediğini doğrular.
    /// </summary>
    /// <param name="password">Doğrulanmak istenen düz metin şifre.</param>
    /// <param name="hashedPassword">Hash'lenmiş şifre.</param>
    /// <returns>Şifreler eşleşiyorsa <c>true</c>, aksi takdirde <c>false</c> döner.</returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}

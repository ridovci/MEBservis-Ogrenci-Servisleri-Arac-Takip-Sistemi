namespace MEBservis.Domain.Interfaces
{
    /// <summary>
    /// Raporlama işlemleri için gerekli veritabanı işlemlerini sağlayan arayüz.
    /// Bu arayüz, kullanıcıya ait takip oturumlarını ve takip verilerini almak için gerekli metodları tanımlar.
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Kullanıcı ID'si ve tarih bilgisine göre takip oturumlarını getirir.
        /// </summary>
        /// <param name="userId">Takip oturumlarını almak istenen kullanıcının benzersiz ID'si.</param>
        /// <param name="date">Oturumların ait olduğu tarih.</param>
        /// <returns>Belirtilen tarih ve kullanıcıya ait takip oturumlarını içeren bir koleksiyon.</returns>
        Task<IEnumerable<TrackingSession?>> GetSessionByUserIdAndDateAsync(int userId, DateTime date);

        /// <summary>
        /// Verilen takip oturum ID'sine göre takip verilerini getirir.
        /// </summary>
        /// <param name="sessionId">Verilerini almak istenen takip oturumunun benzersiz ID'si.</param>
        /// <returns>Belirtilen takip oturumuna ait takip verilerini içeren bir koleksiyon.</returns>
        Task<IEnumerable<TrackingData?>> GetDataBySessionIdAsync(int sessionId);
    }
}

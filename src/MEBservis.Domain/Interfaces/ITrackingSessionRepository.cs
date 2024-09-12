using MEBservis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MEBservis.Domain.Interfaces
{
    /// <summary>
    /// Kullanıcı takip oturumları ve konum verileri için repository arayüzü.
    /// </summary>
    public interface ITrackingSessionRepository
    {
        /// <summary>
        /// Belirtilen kullanıcı ID'sine göre aktif takip oturumunu getirir.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <returns>Aktif takip oturumu veya null.</returns>
        Task<TrackingSession?> GetActiveSessionByUserIdAsync(int userId);

        /// <summary>
        /// Yeni bir takip oturumu ekler.
        /// </summary>
        /// <param name="trackingSession">Eklenecek takip oturumu.</param>
        Task AddTrackingSessionAsync(TrackingSession trackingSession);

        /// <summary>
        /// Yeni bir konum verisi ekler.
        /// </summary>
        /// <param name="trackingData">Eklenecek konum verisi.</param>
        Task AddTrackingDataAsync(TrackingData trackingData);

        /// <summary>
        /// Veritabanında belirtilen kullanıcı ID'sine sahip bir kullanıcı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId">Kontrol edilecek kullanıcının ID'si.</param>
        /// <returns>Kullanıcı mevcutsa <c>true</c>, değilse <c>false</c> döner.</returns>
        Task<bool> UserExistsAsync(int userId);

        /// <summary>
        /// Veritabanındaki mevcut takip oturumunu günceller.
        /// </summary>
        /// <param name="trackingSession">Güncellenecek takip oturumu nesnesi.</param>
        /// <returns>Güncelleme işlemi tamamlandığında bir <see cref="Task"/> döner.</returns>
        Task UpdateTrackingSessionAsync(TrackingSession trackingSession);


    }
}

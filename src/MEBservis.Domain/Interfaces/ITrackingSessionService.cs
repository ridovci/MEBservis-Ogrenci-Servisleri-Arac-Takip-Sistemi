
namespace MEBservis.Application.Services
{
    /// <summary>
    /// Kullanıcı takip oturumlarını yönetmek için servis arayüzü.
    /// </summary>
    public interface ITrackingSessionService
    {
        /// <summary>
        /// Kullanıcı için aktif bir takip oturumu başlatır veya mevcut bir oturumu günceller.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="trackingData">Eklenmesi gereken konum verisi.</param>
        Task StartOrUpdateTrackingSessionAsync(int userId, TrackingData trackingData);

        /// <summary>
        /// Kullanıcıya ait aktif takip oturumunu durdurur.
        /// </summary>
        /// <param name="userId">Takip oturumunu durdurulacak kullanıcının ID'si.</param>
        /// <returns>Güncelleme işlemi tamamlandığında bir <see cref="Task"/> döner.</returns>
        Task StopTrackingSessionAsync(int userId);
    }
}

using MEBservis.Domain.Interfaces;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// Kullanıcı takip oturumları ve konum verilerini yönetmek için servis sınıfı.
    /// </summary>
    public class TrackingSessionService : ITrackingSessionService
    {
        private readonly ITrackingSessionRepository _repository;

        /// <summary>
        /// TrackingSessionService sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="repository">Takip oturumu ve konum verisi için repository.</param>
        public TrackingSessionService(ITrackingSessionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Kullanıcı için aktif bir takip oturumu başlatır veya mevcut bir oturumu günceller.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="trackingData">Eklenmesi gereken konum verisi.</param>
        public async Task StartOrUpdateTrackingSessionAsync(int userId, TrackingData trackingData)
        {
            // Kullanıcının var olup olmadığını kontrol et
            var userExists = await _repository.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new Exception("Geçersiz kullanıcı. Kullanıcı veritabanında bulunamadı.");
            }

            // Aktif oturumu bul
            var activeSession = await _repository.GetActiveSessionByUserIdAsync(userId);

            if (activeSession != null)
            {
                // Aktif oturum varsa, konum verilerini ekle
                trackingData.SessionId = activeSession.SessionId;
                await _repository.AddTrackingDataAsync(trackingData);
            }
            else
            {
                // Aktif oturum yoksa, yeni bir oturum oluştur
                var newSession = new TrackingSession
                {
                    UserId = userId,
                    StartTime = DateTime.Now
                };
                await _repository.AddTrackingSessionAsync(newSession);

                // Yeni oturum için konum verilerini ekle
                trackingData.SessionId = newSession.SessionId;
                await _repository.AddTrackingDataAsync(trackingData);
            }
        }

        /// <summary>
        /// Kullanıcıya ait aktif takip oturumunu durdurur.
        /// </summary>
        /// <param name="userId">Takip oturumunu durdurulacak kullanıcının ID'si.</param>
        /// <exception cref="Exception">Aktif bir takip oturumu bulunamadığında bir istisna fırlatır.</exception>
        /// <returns>Güncelleme işlemi tamamlandığında bir <see cref="Task"/> döner.</returns>
        public async Task StopTrackingSessionAsync(int userId)
        {
            // Kullanıcıya ait aktif oturumu bul
            var activeSession = await _repository.GetActiveSessionByUserIdAsync(userId);
            if (activeSession != null)
            {
                // Eğer aktif bir oturum varsa, EndTime alanını güncelleyerek oturumu kapat
                activeSession.EndTime = DateTime.Now;

                // Değişiklikleri kaydetmek için veritabanını güncelle
                await _repository.UpdateTrackingSessionAsync(activeSession);
            }
            else
            {
                throw new Exception("Aktif bir takip oturumu bulunamadı.");
            }
        }
    }
}

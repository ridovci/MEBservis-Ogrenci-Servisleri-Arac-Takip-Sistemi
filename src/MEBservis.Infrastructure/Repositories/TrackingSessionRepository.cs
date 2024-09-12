using MEBservis.Domain.Interfaces;
using MEBservis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MEBservis.Infrastructure.Repositories
{
    /// <summary>
    /// Kullanıcı takip oturumları ve konum verileri için repository sınıfı.
    /// </summary>
    public class TrackingSessionRepository : ITrackingSessionRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// TrackingSessionRepository sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="context">Veritabanı bağlamı (DbContext).</param>
        public TrackingSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Belirtilen kullanıcı ID'sine göre aktif takip oturumunu getirir.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <returns>Aktif takip oturumu veya null.</returns>
        public async Task<TrackingSession?> GetActiveSessionByUserIdAsync(int userId)
        {
            // Kullanıcıya ait aktif oturumu bulur; eğer yoksa null döner.
            return await _context.TrackingSessions
                .Where(s => s.UserId == userId && s.EndTime == null)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Yeni bir takip oturumu ekler.
        /// </summary>
        /// <param name="trackingSession">Eklenecek takip oturumu.</param>
        public async Task AddTrackingSessionAsync(TrackingSession trackingSession)
        {
            // Yeni takip verisini ekler
            _context.TrackingSessions.Add(trackingSession);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Yeni bir konum verisi ekler.
        /// </summary>
        /// <param name="trackingData">Eklenecek konum verisi.</param>
        public async Task AddTrackingDataAsync(TrackingData trackingData)
        {
            _context.TrackingDatas.Add(trackingData);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Veritabanında belirtilen kullanıcı ID'sine sahip bir kullanıcı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId">Kontrol edilecek kullanıcının ID'si.</param>
        /// <returns>Kullanıcı mevcutsa <c>true</c>, değilse <c>false</c> döner.</returns>
        public async Task<bool> UserExistsAsync(int userId)
        {
            // Veritabanında belirtilen kullanıcı ID'sine sahip bir kullanıcı olup olmadığını kontrol eder
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        /// <summary>
        /// Veritabanındaki mevcut takip oturumunu günceller.
        /// </summary>
        /// <param name="trackingSession">Güncellenecek takip oturumu nesnesi.</param>
        public async Task UpdateTrackingSessionAsync(TrackingSession trackingSession)
        {
            // Veritabanındaki mevcut takip oturumunu günceller
            _context.TrackingSessions.Update(trackingSession);
            await _context.SaveChangesAsync();
        }
    }
}

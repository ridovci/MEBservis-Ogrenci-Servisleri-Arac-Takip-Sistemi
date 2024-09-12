using MEBservis.Domain.Interfaces;
using MEBservis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEBservis.Infrastructure.Repositories
{
    /// <summary>
    /// Rapor verileriyle ilgili veritabanı işlemlerini yöneten sınıf.
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// <see cref="ReportRepository"/> sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="context">Veritabanı ile etkileşim kurmak için kullanılan <see cref="ApplicationDbContext"/> örneği.</param>
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Belirtilen kullanıcı ve tarihe göre takip oturumlarını alır.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="date">Tarih bilgisi.</param>
        /// <returns>Belirtilen kullanıcı ve tarihe göre filtrelenmiş takip oturumları.</returns>
        public async Task<IEnumerable<TrackingSession?>> GetSessionByUserIdAndDateAsync(int userId, DateTime date)
        {
            // Tarih parametresinin başlangıcı (günün ilk saati)
            var startDate = date.Date;

            // Sadece tarih kısmına göre filtreleme yaparak, saat bilgisini göz ardı eder
            return await _context.TrackingSessions
                .Where(x => x.UserId == userId && x.StartTime.Date == startDate)
                .OrderBy(x => x.StartTime) // Oturumları başlangıç zamanına göre sıralar
                .ToListAsync(); // Asenkron olarak listeyi alır
        }

        /// <summary>
        /// Belirtilen oturum ID'sine göre takip verilerini alır.
        /// </summary>
        /// <param name="sessionId">Oturum ID'si.</param>
        /// <returns>Belirtilen oturum ID'sine sahip takip verileri.</returns>
        public async Task<IEnumerable<TrackingData?>> GetDataBySessionIdAsync(int sessionId)
        {
            return await _context.TrackingDatas
                .Where(x => x.SessionId == sessionId) // Oturum ID'sine göre filtreleme yapar
                .ToListAsync(); // Asenkron olarak listeyi alır
        }
    }
}

using MEBservis.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// Rapor servis arayüzü.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Belirtilen kullanıcı ve tarihe göre takip oturumlarını alır.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="date">Tarih.</param>
        /// <returns>Takip oturumları.</returns>
        Task<IEnumerable<TrackingSessionDto>> GetSessionByUserIdAndDateAsync(int userId, DateTime date);

        /// <summary>
        /// Belirtilen oturum ID'sine göre takip verilerini alır.
        /// </summary>
        /// <param name="sessionId">Oturum ID'si.</param>
        /// <returns>Takip verileri.</returns>
        Task<IEnumerable<TrackingDataDto>> GetDataBySessionIdAsync(int sessionId);
    }
}

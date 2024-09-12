using MEBservis.Application.DTOs;
using MEBservis.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MEBservis.Api.Controllers
{
    /// <summary>
    /// Raporları oluşturmak ve almak için API uç noktalarını sağlar.
    /// </summary>
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        /// <summary>
        /// <see cref="ReportController"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="reportService">Rapor servisi örneği.</param>
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Belirtilen kullanıcı ve tarih için raporları alır.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="date">Raporun alınacağı tarih.</param>
        /// <returns>Rapor verileri veya 404 Not Found sonucu.</returns>
        [HttpGet("by-date")]
        public async Task<IActionResult> GetReportAsync(int userId, DateTime date)
        {
            // Servis katmanında raporları al
            var reportSessions = await _reportService.GetSessionByUserIdAndDateAsync(userId, date);

            // Eğer sonuç boşsa veya bulunamadıysa NotFound döndür
            if (reportSessions == null || !reportSessions.Any())
            {
                return NotFound("Kayıt bulunamadı.");
            }

            // Sonuçları döndür
            return Ok(reportSessions);
        }

        /// <summary>
        /// Belirtilen oturum ID'sine göre rapor verilerini alır.
        /// </summary>
        /// <param name="sessionId">Oturum ID'si.</param>
        /// <returns>Rapor verileri veya 404 Not Found sonucu.</returns>
        [HttpGet("session-id/{sessionId}")]
        public async Task<IActionResult> GetReportById(int sessionId)
        {
            // Servis katmanında oturum verilerini al
            var reportData = await _reportService.GetDataBySessionIdAsync(sessionId);

            // Eğer sonuç boşsa veya bulunamadıysa NotFound döndür
            if (reportData == null)
            {
                return NotFound("Kayıt bulunamadı.");
            }

            // Sonuçları döndür
            return Ok(reportData);
        }
    }
}

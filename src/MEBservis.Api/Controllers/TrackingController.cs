using AutoMapper;
using MEBservis.Application.DTOs;
using MEBservis.Application.Services;
using MEBservis.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MEBservis.Api.Controllers
{
    /// <summary>
    /// Kullanıcı takip işlemlerini yönetmek için API kontrolcüsü.
    /// </summary>
    [ApiController]
    [Route("api/trackings")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingSessionService _trackingSessionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// TrackingController sınıfının yapıcı metodu.
        /// </summary>
        /// <param name="trackingSessionService">Takip oturumu servisi.</param>
        public TrackingController(ITrackingSessionService trackingSessionService, IMapper mapper)
        {
            // Takip oturumu servisini örnek değişkene atar.
            _trackingSessionService = trackingSessionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Kullanıcı için bir takip oturumu başlatır veya günceller.
        /// </summary>
        /// <param name="trackingDataDto">Kullanıcı ve konum bilgilerini içeren veri transfer objesi.</param>
        /// <returns>Takip işlemi sonucunu belirten bir HTTP durumu.</returns>
        [HttpPost("start-tracking")]
        public async Task<IActionResult> StartTracking([FromBody] TrackingDataDto trackingDataDto)
        {
            // Geçerli bir veri olup olmadığını kontrol eder
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // AutoMapper kullanarak DTO'yu entity'ye dönüştür
            var trackingData = _mapper.Map<TrackingData>(trackingDataDto);
            trackingData.Timestamp = DateTime.Now;

            // Takip oturumunu başlatır veya mevcut bir oturumu günceller
            await _trackingSessionService.StartOrUpdateTrackingSessionAsync(trackingDataDto.UserId, trackingData);

            return Ok("Takip işlemi başarılı."); // İşlemin başarılı olduğunu belirtir
        }

        /// <summary>
        /// Kullanıcı için aktif olan takip oturumunu durdurur.
        /// </summary>
        /// <param name="userId">Takip oturumu durdurulacak kullanıcının ID'si.</param>
        /// <returns>Takip durdurma işleminin sonucunu belirten bir HTTP durumu.</returns>
        [HttpPost("stop-tracking")]
        public async Task<IActionResult> StopTracking([FromBody] int userId)
        {
            // Geçerli bir kullanıcı ID'si olup olmadığını kontrol eder
            if (userId <= 0)
            {
                return BadRequest("Geçersiz Kullanıcı ID."); // Geçersiz ID durumunda hata döner
            }

            try
            {
                // Takip oturumunu durdurur
                await _trackingSessionService.StopTrackingSessionAsync(userId);
                return Ok("Takip işlemi durduruldu."); // İşlemin başarılı olduğunu belirtir
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message); // Hata durumunda hata mesajını döner
            }
        }
    }
}

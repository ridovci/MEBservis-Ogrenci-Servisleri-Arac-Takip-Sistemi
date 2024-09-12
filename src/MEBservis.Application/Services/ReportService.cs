using MEBservis.Domain.Interfaces;
using MEBservis.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace MEBservis.Application.Services
{
    /// <summary>
    /// Raporlarla ilgili işlemleri yönetir.
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;


        /// <summary>
        /// <see cref="ReportService"/> sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="repository">Rapor verilerini almak için kullanılan repository örneği.</param>
        public ReportService(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Belirtilen kullanıcı ve tarihe göre takip oturumlarını alır.
        /// </summary>
        /// <param name="userId">Kullanıcı ID'si.</param>
        /// <param name="date">Tarih.</param>
        /// <returns>Takip oturumları.</returns>
        /// <exception cref="Exception">Veri bulunamadığında veya başka bir hata oluştuğunda.</exception>
        public async Task<IEnumerable<TrackingSessionDto>> GetSessionByUserIdAndDateAsync(int userId, DateTime date)
        {
            var sessions = await _repository.GetSessionByUserIdAndDateAsync(userId, date);

            // Eğer sonuç null ise özel bir hata mesajı fırlat
            if (sessions == null)
            {
                throw new Exception("Belirtilen tarihte takip bilgisi bulunamadı.");
            }

            // DTO'ya dönüştür ve döndür
            var sessionDtos = _mapper.Map<IEnumerable<TrackingSessionDto>>(sessions);

            return sessionDtos;
        }

        /// <summary>
        /// Belirtilen oturum ID'sine göre takip verilerini alır.
        /// </summary>
        /// <param name="sessionId">Oturum ID'si.</param>
        /// <returns>Takip verileri.</returns>
        /// <exception cref="Exception">Veri bulunamadığında veya başka bir hata oluştuğunda.</exception>
        public async Task<IEnumerable<TrackingDataDto>> GetDataBySessionIdAsync(int sessionId)
        {
            var datas = await _repository.GetDataBySessionIdAsync(sessionId);

            // Eğer sonuç null ise özel bir hata mesajı fırlat
            if (datas == null)
            {
                throw new Exception("Belirtilen oturum ID'sine sahip veri bulunamadı.");
            }

            var dataDtos = _mapper.Map<IEnumerable<TrackingDataDto>>(datas);

            return dataDtos;
        }
    }
}

namespace MEBservis.Application.DTOs
{
  /// <summary>
  /// Kullanıcı takip oturumlarını temsil eden veri transfer nesnesi (DTO).
  /// </summary>
    public class TrackingSessionDto
    {
        /// <summary>
        /// Otomatik artan benzersiz ID.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// İlgili kullanıcı ID'si.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Takibin başlatıldığı zaman.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Takibin bitirildiği zaman (nullable).
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Takip oturumuna ait konum verileri.
        /// </summary>
    }
}
namespace MEBservis.Application.Utilities

{
    /// <summary>
    /// Sayfalama sonuçlarını temsil eden sınıf.
    /// </summary>
    /// <typeparam name="T">Sayfalama sonuçları türü.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Sayfada bulunan öğeler.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Toplam öğe sayısı.
        /// </summary>
        public int TotalCount { get; set; }
    }
}

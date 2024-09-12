using Microsoft.EntityFrameworkCore;

namespace MEBservis.Infrastructure.Data
{
    /// <summary>
    /// Entity Framework Core veri tabanı bağlamını temsil eder.
    /// Bu sınıf, veri tabanı ile etkileşim kurmak ve veri modeli yapılandırması yapmak için kullanılır.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Yeni bir örneğini başlatır ve veri tabanı bağlamı için yapılandırma seçeneklerini alır.
        /// </summary>
        /// <param name="options">DbContextOptions<ApplicationDbContext> türünde yapılandırma seçenekleri.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Kullanıcıları temsil eden veri kümesi.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Kullanıcı rollerini temsil eden veri kümesi.
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Takip oturumlarını temsil eden veri kümesi.
        /// </summary>
        public DbSet<TrackingSession> TrackingSessions { get; set; }

        /// <summary>
        /// Takip verilerini temsil eden veri kümesi.
        /// </summary>
        public DbSet<TrackingData> TrackingDatas { get; set; }

        /// <summary>
        /// Model yapılandırmasını özelleştirir.
        /// Bu metod, veri modelinin veri tabanına nasıl karşılık geldiğini yapılandırmak için kullanılır.
        /// </summary>
        /// <param name="modelBuilder">Model yapılandırma araçlarını sağlayan ModelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Eğer UserRole tablosunda kayıt yoksa, varsayılan kayıtları ekle
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, UserRoleName = "user" },
                new UserRole { UserRoleId = 2, UserRoleName = "admin" }
            );
        }
    }
}

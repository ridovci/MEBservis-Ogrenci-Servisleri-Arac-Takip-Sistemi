using MEBservis.Domain.Interfaces;
using MEBservis.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MEBservis.Infrastructure.Repositories
{
    /// <summary>
    /// UserRepository sınıfı, kullanıcı verileri ile ilgili CRUD operasyonlarını yönetir.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// UserRepository sınıfının bir örneğini oluşturur.
        /// </summary>
        /// <param name="context">Uygulama veri bağlamı.</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Veritabanındaki tüm kullanıcıları alır.
        /// </summary>
        /// <returns>Kullanıcıların bir koleksiyonu.</returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Veritabanında verilen ID'ye sahip kullanıcıyı bulur.
        /// </summary>
        /// <param name="id">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcı nesnesi veya null.</returns>
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// Veritabanında verilen TC Kimlik Numarasına sahip kullanıcıyı bulur.
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik Numarası.</param>
        /// <returns>Kullanıcı nesnesi veya null.</returns>
        public async Task<User?> GetUserByTcKimlikNoAsync(string tcKimlikNo)
        {
            return await _context.Users.FirstOrDefaultAsync(t => t.TcKimlikNo == tcKimlikNo);
        }

        /// <summary>
        /// Verilen TC Kimlik Numarasının veritabanında mevcut olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="tcKimlikNo">TC Kimlik Numarası.</param>
        /// <returns>TC Kimlik Numarasının var olup olmadığını belirten bir değer.</returns>
        public async Task<bool> TcKimlikNoExistsAsync(string tcKimlikNo)
        {
            return await _context.Users.AnyAsync(u => u.TcKimlikNo == tcKimlikNo);
        }

        /// <summary>
        /// Verilen araç plaka numarasının veritabanında mevcut olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="vehiclePlateNo">Araç plaka numarası.</param>
        /// <returns>Araç plaka numarasının var olup olmadığını belirten bir değer.</returns>
        public async Task<bool> VehiclePlateNoExistsAsync(string vehiclePlateNo)
        {
            return await _context.Users.AnyAsync(u => u.VehiclePlateNo == vehiclePlateNo);
        }

        /// <summary>
        /// Verilen kullanıcıyı veritabanına ekler.
        /// </summary>
        /// <param name="user">Eklenecek kullanıcı nesnesi.</param>
        /// <returns>Kullanıcının başarıyla eklenip eklenmediğini belirten bir değer.</returns>
        public async Task<bool> AddUserAsync(User user)
        {
            if (await TcKimlikNoExistsAsync(user.TcKimlikNo) || await VehiclePlateNoExistsAsync(user.VehiclePlateNo))
            {
                return false;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Veritabanındaki mevcut bir kullanıcıyı günceller.
        /// </summary>
        /// <param name="user">Güncellenecek kullanıcı nesnesi.</param>
        /// <returns>Kullanıcının başarıyla güncellenip güncellenmediğini belirten bir değer.</returns>
        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);

            if (existingUser == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                existingUser.PhoneNumber = user.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(user.VehiclePlateNo))
            {
                existingUser.VehiclePlateNo = user.VehiclePlateNo;
            }

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                existingUser.PasswordHash = user.PasswordHash;
            }

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return true;
        }


        /// <summary>
        /// Kullanıcının admin olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="userId">Kullanıcının ID'si.</param>
        /// <returns>Kullanıcının admin olup olmadığını belirten bir değer.</returns>
        public async Task<bool> IsAdminAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null && user.RoleId == 2;
        }
    }
}

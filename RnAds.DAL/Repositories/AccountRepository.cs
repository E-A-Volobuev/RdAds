using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly RnAdsContext _context;
        public AccountRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsyncUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// проверка,есть ли в бд пользователь с таким же логином
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> ExistByNameAsync(RegisterDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// получение пользователя по логину и паролю
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> GetUserByLoginAndPasswordAsync(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == password);
            return user;
        }
        /// <summary>
        /// получение пользователя по логину
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByLoginAsync(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}

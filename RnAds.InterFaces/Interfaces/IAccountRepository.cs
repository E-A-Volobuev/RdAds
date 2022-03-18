using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public  interface IAccountRepository : IGenericRepository<User>
    {
        /// <summary>
        /// проверка,есть ли в бд пользователь с таким же логином
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> ExistByNameAsync(RegisterDTO dto);
        /// <summary>
        /// получение пользователя по логину и паролю
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAndPasswordAsync(string username, string password);

        Task CreateAsyncUser(User user);

        /// <summary>
        /// получение пользователя по логину
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAsync(string email);
    }
}

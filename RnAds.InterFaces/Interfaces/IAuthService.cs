using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RnAds.InterFaces.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// генерация токена доступа
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        string EncodedJwtToken(ClaimsIdentity identity);

        /// <summary>
        /// создание объекта claim для авторизации
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> GetIdentityAsync(string username, string password);

        /// <summary>
        /// регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<User> CreateAccountAsync(RegisterDTO dto);

        /// <summary>
        /// получение id текущего пользователя
        /// </summary>
        /// <returns></returns>
        Task<Guid> GetCurrentUserIdAsync(string email);

        /// <summary>
        /// получение имени  пользователя по id
        /// </summary>
        /// <returns></returns>
        Task<string> GetSelectUserNameAsync(Guid id);

        // <summary>
        /// получение текущего пользователя по email
        /// </summary>
        /// <returns></returns>
        Task<User> GetCurrentUser(string email);

        /// <summary>
        /// редактирование текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task EditCurrentUser(DtoUser user);
    }
}

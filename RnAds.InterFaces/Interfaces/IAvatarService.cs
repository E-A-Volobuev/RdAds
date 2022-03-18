using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IAvatarService
    {
        /// <summary>
        /// получение аватара пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetAvatarAsync(Guid id);

        /// <summary>
        /// добавление аватара пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task AddAvatarAsync(Guid id, IFormFile photo);

    }
}

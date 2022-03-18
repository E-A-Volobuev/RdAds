using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IPictureService
    {
        /// <summary>
        /// получение фотографии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetPhotoAsync(Guid id);

        /// <summary>
        /// добавление фотографий
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        Task AddPhotoAsync(Guid id, IFormFile[] photo,bool isMessage);

        /// <summary>
        /// обновление фотографий объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picturesId"></param>
        /// <param name="photo"></param>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        Task UpdatePhotoAsync(Guid id, Guid[] picturesId, IFormFile[] photo,bool isMessage);
    }
}

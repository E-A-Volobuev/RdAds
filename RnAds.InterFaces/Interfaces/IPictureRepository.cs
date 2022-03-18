using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IPictureRepository : IGenericRepository<Picture>
    {
        /// <summary>
        /// удаление всех фотографий объявления,кроме тех,которые оставил пользователь
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picturesId"></param>
        /// <returns></returns>
        Task DeleteAllPhotoByIdAdAsync(Guid id, Guid[] picturesId);

        /// <summary>
        /// получение картинок текущего сообщения
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task<List<Picture>> GetPicturesCurrentMessage(Guid messageId);
    }
}

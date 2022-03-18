using RnAds.Model.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IUserAdService
    {
        /// <summary>
        /// отправляем запрос с ip нашего пк для получения местоположения (получение страны и города)
        /// </summary>
        /// <returns></returns>
        string GetResponceJsonByCity();

        /// <summary>
        /// получение всех объявлений текущего пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<DtoPartialAd>> GetAllAdByUser(Guid userId);

        /// <summary>
        /// добавление объявления в избранные
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        Task AddFavorite(Guid userId, Guid adId);

        /// <summary>
        /// удаление объявления из избранных
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        Task DeleteFavorite(Guid userId, Guid adId);

        /// <summary>
        /// получение всех избранных объявлений пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<DtoPartialAd>> GetAllFavorite(Guid userId);
    }
}

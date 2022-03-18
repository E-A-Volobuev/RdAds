using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<FavoriteUserId>
    {
        /// <summary>
        /// получение всех избранных объявлений пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Ad>> GetAllFavoriteAd(Guid userId);

        /// <summary>
        /// существует ли запись по id пользователя и id объявления
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        Task<bool> ExistByUserIdAndAdIdAsync(Guid userId, Guid adId);

        /// <summary>
        /// получение избранного по id пользователя и id объявления
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        Task<FavoriteUserId> GetFavoriteAdByUserIdAsync(Guid userId, Guid adId);
    }
}

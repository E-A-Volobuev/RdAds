using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IAdRepository : IGenericRepository<Ad>
    {
        /// <summary>
        /// получение объявления по id со связанными фотографиями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Ad> GetByIdAndPhotosAsync(Guid id);


        /// <summary>
        /// получение объявления со списком пользователей,которые добавили его в избранное
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Ad> GetAdByFavorite(Guid id);

        /// <summary>
        /// получение всех объявлений текущего пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Ad>> GetAllAdByCurrentUser(Guid userId);


        /// <summary>
        /// получение объявлений определенной в определённом регионе по определённому типу
        /// </summary>
        /// <param name="type"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<List<Ad>> GetListAdByRegionByTypeAsync(AdQueryRequest adQuery);

        /// <summary>
        /// получение всех объявлений со связанными фотографиями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<Ad>> GetAllAdAndPhotosAsync();
    }
}

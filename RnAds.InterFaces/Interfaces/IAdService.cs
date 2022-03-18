using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IAdService
    {
        /// <summary>
        /// получение объявления по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DtoAd> GetByIdAdAsync(Guid id);

        /// <summary>
        /// вывод объявлений определенной категории
        /// </summary>
        /// <param name="adQuery"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<DtoPartialAd>> GetListAdPartialAsync(AdQueryRequest adQuery, Guid userId);

        /// <summary>
        /// создание объявления 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Guid> CreateAdAsync(DtoAd dto, Guid userId);

        /// <summary>
        /// редактирование объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task UpdateAdAsync(DtoAd dto, Guid userId);

        /// <summary>
        /// удаление объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task DeleteAdAsync(DtoAd dto);

        /// <summary>
        /// получение всех объявлений
        /// </summary>
        /// <param name="isAuthorise"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<DtoPartialAd>> GetListAllAdPartialAsync(Guid userId);

        /// <summary>
        /// получение количества объявлений по категориям
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        Task<List<int>> GetAdsCount(string city);

        /// <summary>
        /// хэлпер создания частичных обьявлений 
        /// </summary>
        /// <param name="listAd"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<DtoPartialAd>> CreateDtoListHelper(List<Ad> listAd, Guid userId);

    }
}

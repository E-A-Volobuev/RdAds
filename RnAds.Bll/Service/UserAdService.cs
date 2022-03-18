using Newtonsoft.Json.Linq;
using RnAds.Core.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class UserAdService : IUserAdService
    {
        private readonly IAdRepository _iAdRepository;
        private readonly IFavoriteRepository _iFavoriteRepository;
        private readonly IAdService _adService;

        public UserAdService(IAdRepository iAdRepository, IAdService adService, IFavoriteRepository iFavoriteRepository)
        {
            _iAdRepository = iAdRepository;
            _adService = adService;
            _iFavoriteRepository = iFavoriteRepository;
        }


        /// <summary>
        /// отправляем запрос с ip нашего пк для получения местоположения (получение страны и города)
        /// </summary>
        /// <returns></returns>
        public string GetResponceJsonByCity()
        {
            //получаем внешний ip-адрес компьютера
            string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");

            string url = $"http://free.ipwhois.io/json/" + pubIp + "?lang=ru";
            string result = string.Empty;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = HttpMethod.Get.ToString();

            req.UserAgent = "SimpleHostClient";
            req.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            using (StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            string city = GetCities(result);
            return city;

        }

        /// <summary>
        /// получение города
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string GetCities(string json)
        {
            var result = JObject.Parse(json);
            var city = (string)result["city"];
            return city;
        }

        /// <summary>
        /// получение всех объявлений текущего пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DtoPartialAd>> GetAllAdByUser(Guid userId)
        {
            List<DtoPartialAd> dtoList = new List<DtoPartialAd>();

            if (userId != null && userId != Guid.Empty)
            {
                var usersAd = await _iAdRepository.GetAllAdByCurrentUser(userId);

                if (usersAd != null)
                    dtoList = await _adService.CreateDtoListHelper(usersAd, userId);
            }

            return dtoList;
        }

        /// <summary>
        /// добавление объявления в избранные
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        public async Task AddFavorite(Guid userId, Guid adId)
        {
            var favorite = await _iFavoriteRepository.ExistByUserIdAndAdIdAsync(userId, adId);

            if (!favorite)
            {
                FavoriteUserId fav = new FavoriteUserId();
                fav.UserId = userId;
                fav.AdId = adId;

                if (fav != null)
                    await _iFavoriteRepository.CreateAsync(fav);
            }

        }
        /// <summary>
        /// удаление объявления из избранных
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        public async Task DeleteFavorite(Guid userId, Guid adId)
        {
            var favorite = await _iFavoriteRepository.ExistByUserIdAndAdIdAsync(userId, adId);

            if (favorite)
            {
                var fav = await _iFavoriteRepository.GetFavoriteAdByUserIdAsync(userId, adId);
                if (fav != null)
                    await _iFavoriteRepository.DeleteAsync(fav);
            }
        }
        /// <summary>
        /// получение всех избранных объявлений пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DtoPartialAd>> GetAllFavorite(Guid userId)
        {
            List<DtoPartialAd> dtoList = new List<DtoPartialAd>();

            if (userId != null && userId != Guid.Empty)
            {
                var favorite = await _iFavoriteRepository.GetAllFavoriteAd(userId);

                if (favorite != null)
                    dtoList = await _adService.CreateDtoListHelper(favorite, userId);
            }

            return dtoList;
        }

    }
}

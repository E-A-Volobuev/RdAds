using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class FavoriteRepository : GenericRepository<FavoriteUserId>, IFavoriteRepository
    {
        private readonly RnAdsContext _context;
        public FavoriteRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// получение всех избранных объявлений пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Ad>> GetAllFavoriteAd(Guid userId)
        {
            List<Ad> favoriteAds = new List<Ad>();
            ///получаем список id объявлений, которые пользователь указал как избранные
            var listAdId = await _context.FavoriteUserIdes.Where(x => x.UserId == userId).Select(x => x.AdId).ToListAsync();
            if (listAdId != null)
            {
                foreach (var s in listAdId)
                {
                    var currentAd = await _context.Ads.Include(x=>x.Pictures).FirstOrDefaultAsync(x => x.Id == s);

                    if (currentAd != null)
                        favoriteAds.Add(currentAd);
                }
            }

            return favoriteAds;
        }

        /// <summary>
        /// существует ли запись по id пользователя и id объявления
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        public async Task<bool> ExistByUserIdAndAdIdAsync(Guid userId, Guid adId)
        {
            var favorite = await _context.FavoriteUserIdes.FirstOrDefaultAsync(x => x.UserId == userId && x.AdId == adId);
            if (favorite == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// получение избранного по id пользователя и id объявления
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="adId"></param>
        /// <returns></returns>
        public async Task<FavoriteUserId> GetFavoriteAdByUserIdAsync(Guid userId, Guid adId)
        {
            var favorite = await _context.FavoriteUserIdes.FirstOrDefaultAsync(x => x.UserId == userId && x.AdId == adId);

            if (favorite != null)
                return favorite;
            else
                return null;
        }
    }
}

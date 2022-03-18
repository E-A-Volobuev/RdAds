using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class AdRepository : GenericRepository<Ad>, IAdRepository
    {
        private readonly RnAdsContext _context;
        public AdRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// получение объявления по id со связанными фотографиями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ad> GetByIdAndPhotosAsync(Guid id)
        {
            var ad = await _context.Ads.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
            ad.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == ad.UserId);
            return ad;
        }

        /// <summary>
        /// получение объявлений определенной в определённом регионе по определённому типу
        /// </summary>
        /// <param name="type"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<List<Ad>> GetListAdByRegionByTypeAsync(AdQueryRequest adQuery)
        {
            int index = Convert.ToInt32(adQuery.type);

            AdType type = (AdType)Enum.GetValues(typeof(AdType)).GetValue(index);

            var query = _context.Ads
                .Include(x => x.Pictures)
                .AsQueryable();

            // фильтр по городу
            if (!string.IsNullOrEmpty(adQuery.city))
                query = query.Where(e => e.City == adQuery.city || e.Country == adQuery.city);

            // фильтр по типу объявления
            if (!string.IsNullOrEmpty(adQuery.type))
                query = query.Where(e => e.TypeAd == type);

            // фильтр по названию объявления
            if (!string.IsNullOrEmpty(adQuery.text))
                query = query.Where(e => e.NameAd.Contains(adQuery.text));

            ///фильтр "только с фото"
            if(adQuery.isPhoto)
                query.Where(e => e.Pictures.Count != 0);


            return await query.ToListAsync();


        }

        /// <summary>
        /// получение объявления со списком пользователей,которые добавили его в избранное
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ad> GetAdByFavorite(Guid id)
        {
            var currentAd = await _context.Ads.Include(x => x.ListUserId).FirstOrDefaultAsync(x => x.Id == id);
            return currentAd;
        }

        /// <summary>
        /// получение всех объявлений текущего пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Ad>> GetAllAdByCurrentUser(Guid userId)
        {
            var list = await _context.Ads.Include(x => x.Pictures).Where(x => x.UserId == userId).ToListAsync();
            return list;
        }

        /// <summary>
        /// получение всех объявлений со связанными фотографиями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Ad>> GetAllAdAndPhotosAsync()
        {
            var ad = await _context.Ads.Include(x => x.Pictures).ToListAsync();
            return ad;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class PictureRepository : GenericRepository<Picture>, IPictureRepository
    {
        private readonly RnAdsContext _context;
        public PictureRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// удаление всех фотографий объявления,кроме тех,которые оставил пользователь
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picturesId"></param>
        /// <returns></returns>
        public async Task DeleteAllPhotoByIdAdAsync(Guid id,Guid[] picturesId)
        {
            var ad = await _context.Ads.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
            var picturesForDelete = new List<Picture>();
            foreach(var picture in ad.Pictures)
            {
                foreach (var picId in picturesId)
                {
                    var pic = await _context.Pictures.FirstOrDefaultAsync(x => x.Id != picId && x.Id == picture.Id);
                    if (pic != null)
                    {
                        picturesForDelete.Add(pic);
                    }
                }
            }
            await DeleteRangeAsync(picturesForDelete);

        }

        /// <summary>
        /// получение картинок текущего сообщения
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<List<Picture>> GetPicturesCurrentMessage(Guid messageId)
        {
            var pictures = await _context.Pictures.Where(x => x.MessageId == messageId).ToListAsync();

            if (pictures != null)
                return pictures;
            else
                return null;
        }
    }
}

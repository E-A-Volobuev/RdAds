using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class AvatarRepository : GenericRepository<AvatarUser>, IAvatarRepository
    {
        private readonly RnAdsContext _context;
        public AvatarRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }
        public async Task<AvatarUser> GetAvatarAsync(Guid id)
        {
            var avatar = await _context.Avatars.FirstOrDefaultAsync(x => x.UserId == id.ToString());
            return avatar;
        }
    }
}

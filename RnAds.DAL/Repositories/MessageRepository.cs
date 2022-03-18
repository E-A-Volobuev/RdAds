using RnAds.Core.Interfaces;
using RnAds.Model.Model;

namespace RnAds.DAL.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly RnAdsContext _context;
        public MessageRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }
    }
}

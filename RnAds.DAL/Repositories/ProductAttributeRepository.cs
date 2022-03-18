using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class ProductAttributeRepository : GenericRepository<ProductAttribute>, IProductAttributeRepository
    {
        private readonly RnAdsContext _context;

        public ProductAttributeRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// поиск категорий для объявления
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        public async Task<ProductAttribute> GetByIdAdAsync(Ad ad)
        {
            var product = await _context.ProductAttributes.FirstOrDefaultAsync(x => x.AdId == ad.Id);
            return product;
        }
    }
}

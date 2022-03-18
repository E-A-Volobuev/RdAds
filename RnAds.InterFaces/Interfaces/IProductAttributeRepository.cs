using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IProductAttributeRepository : IGenericRepository<ProductAttribute>
    {
        /// <summary>
        /// поиск категорий для объявления
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        Task<ProductAttribute> GetByIdAdAsync(Ad ad);
    }
}

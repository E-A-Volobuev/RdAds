using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IAvatarRepository : IGenericRepository<AvatarUser>
    {
        /// <summary>
        /// получение аватара пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AvatarUser> GetAvatarAsync(Guid id);
    }
}

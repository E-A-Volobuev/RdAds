using RnAds.InterFaces.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IDialogRepository : IGenericRepository<Dialog>
    {
        /// <summary>
        /// поиск диалога по участникам диалога
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        Task<Dialog> GetDialogByUsersAsync(Guid currentUserId, Guid toUserId);

        /// <summary>
        /// получение диалогов текущего пользователя
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task<List<Dialog>> GetAllDialogCurrentUserAsync(Guid currentUserId);
    }
}

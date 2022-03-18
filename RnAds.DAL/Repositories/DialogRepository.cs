using Microsoft.EntityFrameworkCore;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RnAds.DAL.Repositories
{
    public class DialogRepository : GenericRepository<Dialog>, IDialogRepository
    {
        private readonly RnAdsContext _context;
        public DialogRepository(RnAdsContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// поиск диалога по участникам диалога
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        public async Task<Dialog> GetDialogByUsersAsync(Guid currentUserId, Guid toUserId)
        {

            var dialog = await _context.Dialogs.FirstOrDefaultAsync(FilterByUsersId(currentUserId, toUserId));
            if (dialog != null)
                return dialog;
            else
                return null;
        }
        /// <summary>
        /// фильтр для поиска диалогов пользователя
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        private static Expression<Func<Dialog, bool>> FilterByUsersId(Guid currentUserId, Guid toUserId)
        {
            return x => (x.CurrentUserId == currentUserId && x.ToUserId == toUserId) || (x.CurrentUserId == toUserId && x.ToUserId == currentUserId);
        }
        /// <summary>
        /// получение диалогов текущего пользователя
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<List<Dialog>> GetAllDialogCurrentUserAsync(Guid currentUserId)
        {
            var dialogList = await _context.Dialogs.Include(x => x.Messages).ThenInclude(x=>x.Pictures).Where(x => x.CurrentUserId == currentUserId || x.ToUserId == currentUserId).ToListAsync();
            if (dialogList != null)
                return dialogList;
            else
                return null;
        }

        private bool FlagHelper(Dialog dialog, Guid currentUserId, Guid toUserId)
        {
            bool flag = true;

            if (dialog.CurrentUserId != currentUserId && dialog.ToUserId != toUserId)
                flag = false;
            if (dialog.CurrentUserId != toUserId && dialog.ToUserId != currentUserId)
                flag = false;

            return flag;
        }
    }
}

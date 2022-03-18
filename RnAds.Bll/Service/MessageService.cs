using Microsoft.AspNetCore.Http;
using RnAds.Core.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;
        private readonly IAccountRepository _repoAccount;
        private readonly IPictureService _iPictureService;
        public MessageService(IMessageRepository repo, IAccountRepository repoAccount, IPictureService iPictureService)
        {
            _repo = repo;
            _repoAccount = repoAccount;
            _iPictureService = iPictureService;
        }

        /// <summary>
        /// создание сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dialogId"></param>
        /// <param name="currentUserId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public async Task<Message> WriteMessageAsync(DtoSenderMessage message, Guid dialogId, Guid currentUserId, IFormFile[] photo)
        {
            DateTime now = DateTime.Now;
            var currentUser = await _repoAccount.GetByIdAsync(currentUserId);
            var receiverUser = await _repoAccount.GetByIdAsync(message.ToUserId);

            Message mes = new Message();
            mes.Text = message.Text;
            mes.Time = now.ToString("F");
            mes.DialogId = dialogId;
            mes.ReceiverUserName = receiverUser.Name;
            mes.SenderUserName = currentUser.Name;

            await _repo.CreateAsync(mes);

            if (photo!=null)
                await _iPictureService.AddPhotoAsync(mes.Id, photo, true);

            return mes;
        }
    }
}

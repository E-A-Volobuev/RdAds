using Microsoft.AspNetCore.Http;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class DialogService : IDialogService
    {
        private readonly IDialogRepository _repo;
        private readonly IMessageService _service;
        private readonly IAccountRepository _repoAcc;
        private readonly IAuthService _iAuthService;
        private readonly IPictureRepository _iPictureRepo;

        public DialogService(IDialogRepository repo, IMessageService service, IAccountRepository repoAcc, IAuthService iAuthService,IPictureRepository iPictureRepo)
        {
            _repo = repo;
            _service = service;
            _repoAcc = repoAcc;
            _iAuthService = iAuthService;
            _iPictureRepo = iPictureRepo;
        }
        /// <summary>
        /// создание или обновление диалога
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public async Task<Message> CreateOrUpdateDialogAsync(DtoSenderMessage message, Guid userId, IFormFile[] photo)
        {
            var dialog = await _repo.GetDialogByUsersAsync(userId, message.ToUserId);
            if (dialog == null)
            {
                Dialog newDialog = new Dialog();
                newDialog.CurrentUserId = userId;
                newDialog.ToUserId = message.ToUserId;

                await _repo.CreateAsync(newDialog);

                return await _service.WriteMessageAsync(message, newDialog.Id, userId,photo);
            }
            else
                return await _service.WriteMessageAsync(message, dialog.Id, userId,photo);

        }
        /// <summary>
        ///  получение  диалогов текущего пользователя
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<List<DtoDialog>> GetAllDialogAsync(Guid currentUserId)
        {
            var listDialog = new List<DtoDialog>();
            ///все диалоги пользователя
            var dialogsCurrentUser = await _repo.GetAllDialogCurrentUserAsync(currentUserId);

            await GetNameUsersByIdAsync(dialogsCurrentUser, currentUserId, listDialog);

            return listDialog;
        }
        /// <summary>
        ///  получение имён участников диалогов текущего пользователя
        /// </summary>
        /// <param name="dialogsCurrentUser"></param>
        /// <param name="currentUserId"></param>
        /// <param name="dialogsUserId"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        private async Task GetNameUsersByIdAsync(List<Dialog> dialogsCurrentUser, Guid currentUserId, List<DtoDialog> listDialog)
        {
            ///добавляем в лист всех диалогов id всех участников
            foreach (var dialog in dialogsCurrentUser)
            {
                if (dialog.ToUserId != currentUserId)
                    await HelperDialogAsync(dialog.ToUserId, dialog, listDialog);

                if (dialog.CurrentUserId != currentUserId)
                    await HelperDialogAsync(dialog.CurrentUserId, dialog, listDialog);
            }
            ///удаление дубликатов
            listDialog = listDialog.GroupBy(i => i.NameUser).Select(i => i.FirstOrDefault()).ToList();

        }
        /// <summary>
        /// из сущности диалога преобразуем данные в дто диалогов и сообщений
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dialog"></param>
        /// <param name="listDialog"></param>
        /// <returns></returns>
        private async Task HelperDialogAsync(Guid userId, Dialog dialog, List<DtoDialog> listDialog)
        {
            DtoDialog dto = new DtoDialog();
            var user = await _repoAcc.GetByIdAsync(userId);

            if (user != null)
            {
                dto.NameUser = user.Name;
                dto.LoginUser = user.Email;
            }    
            if (dialog.Messages.Count != 0)
            {
                var lastMessage = dialog.Messages.Last();
                dto.TextMessage = lastMessage.Text;
                dto.Time = lastMessage.Time;

                foreach (var s in dialog.Messages)
                {
                    DtoDialogMessage dtoMessage = new DtoDialogMessage();
                    dtoMessage.ReceiverUserName = s.ReceiverUserName;
                    dtoMessage.SenderUserName = s.SenderUserName;
                    dtoMessage.Text = s.Text;
                    dtoMessage.Time = s.Time;
                    if(s.Pictures.Count!=0)
                    {
                        var picsId = s.Pictures.Select(x => x.Id).ToArray();
                        dtoMessage.PicturesId = picsId;
                    }

                    if (dtoMessage != null)
                        dto.Messages.Add(dtoMessage);
                }
            }
            if (dto != null)
                listDialog.Add(dto);
        }

        /// <summary>
        /// сохранение в бд полученного сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserId"></param>
        /// <param name="userId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public async Task<Message> SaveMessageHelperAsync(string message, Guid toUserId, Guid userId, IFormFile[] photo)
        {
            DtoSenderMessage dto = new DtoSenderMessage();
            dto.Text = message;
            dto.ToUserId = toUserId;

            return await CreateOrUpdateDialogAsync(dto, userId, photo);
        }

        /// <summary>
        /// создание дто отправляемого сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DtoDialogMessage> SendMessageHelperTwoAsync(Message message, Guid toUserId, Guid userId)
        {
            var currentUserName = await _iAuthService.GetSelectUserNameAsync(userId);
            var toUserName = await _iAuthService.GetSelectUserNameAsync(toUserId);

            ///получаем все картинки сообщения
            var picturesCurrentMessage = await _iPictureRepo.GetPicturesCurrentMessage(message.Id);
            var picturesId = picturesCurrentMessage.Select(x => x.Id).ToArray();

            DateTime now = DateTime.Now;

            DtoDialogMessage incomingMessage = new DtoDialogMessage();
            incomingMessage.ReceiverUserName = toUserName;
            incomingMessage.SenderUserName = currentUserName;
            incomingMessage.Text = message.Text;
            incomingMessage.Time = now.ToString("F");
            incomingMessage.PicturesId = picturesId;


            return incomingMessage;
        }
    }
}

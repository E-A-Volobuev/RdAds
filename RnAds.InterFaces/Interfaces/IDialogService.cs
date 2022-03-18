using Microsoft.AspNetCore.Http;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IDialogService
    {
        /// <summary>
        /// создание или обновление диалога
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<Message> CreateOrUpdateDialogAsync(DtoSenderMessage message, Guid userId, IFormFile[] photo);

        /// <summary>
        ///  получение  диалогов текущего пользователя
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task<List<DtoDialog>> GetAllDialogAsync(Guid currentUserId);

        /// <summary>
        /// сохранение в бд полученного сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserId"></param>
        /// <param name="userId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<Message> SaveMessageHelperAsync(string message, Guid toUserId, Guid userId, IFormFile[] photo);

        /// <summary>
        /// создание дто отправляемого сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="toUserId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DtoDialogMessage> SendMessageHelperTwoAsync(Message message,Guid toUserId, Guid userId);
    }
}

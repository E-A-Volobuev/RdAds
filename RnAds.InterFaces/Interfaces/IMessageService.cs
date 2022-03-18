using Microsoft.AspNetCore.Http;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Threading.Tasks;

namespace RnAds.Core.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// создание сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dialogId"></param>
        /// <param name="currentUserId"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<Message> WriteMessageAsync(DtoSenderMessage message, Guid dialogId, Guid currentUserId, IFormFile[] photo);
    }
}

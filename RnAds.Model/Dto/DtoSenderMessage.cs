using Microsoft.AspNetCore.Http;
using System;

namespace RnAds.Model.Dto
{
    public class DtoSenderMessage
    {
        /// <summary>
        /// кому (id пользователя)
        /// </summary>
        public Guid ToUserId { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// кому (имя пользователя)
        /// </summary>
        public string ToUserName { get; set; }
    }
}

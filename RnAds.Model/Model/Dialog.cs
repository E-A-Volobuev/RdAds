using System;
using System.Collections.Generic;

namespace RnAds.Model.Model
{
    public class Dialog
    {
        public Guid Id { get; set; }

        /// <summary>
        /// текущий пользователь
        /// </summary>
        public Guid CurrentUserId { get; set; }
        /// <summary>
        /// собеседник
        /// </summary>
        public Guid ToUserId { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>(); // сообщения диалога
    }
}

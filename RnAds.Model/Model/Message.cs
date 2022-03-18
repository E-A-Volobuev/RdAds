using System;
using System.Collections.Generic;

namespace RnAds.Model.Model
{
    public class Message
    {
        public Guid Id { get; set; }

        /// <summary>
        /// отправитель
        /// </summary>
        public string SenderUserName { get; set; }
        /// <summary>
        /// получатель
        /// </summary>
        public string ReceiverUserName { get; set; }

        public string Text { get; set; }
        /// <summary>
        /// дата и время отправки сообщения
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// фотографии в сообщении
        /// </summary>
        public List<Picture> Pictures { get; set; } = new List<Picture>();

        public Guid DialogId { get; set; }
        public Dialog Dialog { get; set; }
    }
}

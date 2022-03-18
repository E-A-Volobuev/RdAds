
using System;

namespace RnAds.Model.Dto
{
    public class DtoDialogMessage
    {
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
        /// id картинок сообщения
        /// </summary>
        public Guid [] PicturesId { get; set; }
    }
}

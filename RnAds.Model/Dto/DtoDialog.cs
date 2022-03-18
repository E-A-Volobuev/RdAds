using System.Collections.Generic;

namespace RnAds.Model.Dto
{
    public class DtoDialog
    {
        /// <summary>
        /// имя, с кем диалог текущего пользователя
        /// </summary>
        public string NameUser { get; set; }

        /// <summary>
        /// логин пользователя , с кем диалог текущего пользователя
        /// </summary>
        public string LoginUser { get; set; }

        /// <summary>
        /// текст крайнего сообщения в диалоге
        /// </summary>
        public string TextMessage { get; set; }

        /// <summary>
        /// дата и время отправки крайнего сообщения в диалоге
        /// </summary>
        public string Time { get; set; }
        public List<DtoDialogMessage> Messages { get; set; } = new List<DtoDialogMessage>();
    }
}

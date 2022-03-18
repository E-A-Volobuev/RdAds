using System;

namespace RnAds.Model.Model
{
    /// <summary>
    /// фото для объявлений или сообщений
    /// </summary>
    public class Picture
    {
        public Guid Id { get; set; }

        public string FilePath { get; set; }

        public Guid? AdId { get; set; }
        public Ad Ad { get; set; }

        public Guid? MessageId { get; set; }
        public Message Message { get; set; }


    }
}

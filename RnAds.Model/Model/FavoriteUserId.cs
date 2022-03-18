using System;

namespace RnAds.Model.Model
{
    /// <summary>
    /// сущность,показывающая добавлено ли объявление в избранные
    /// </summary>
    public class FavoriteUserId
    {
        public Guid Id { get; set; }

        /// <summary>
        /// id пользователей,которые добавили объявление в избранные
        /// </summary>
        public Guid UserId { get; set; }
        public Guid? AdId { get; set; }
        public Ad Ad { get; set; }
    }
}

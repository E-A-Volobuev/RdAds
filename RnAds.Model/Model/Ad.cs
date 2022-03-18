using System;
using System.Collections.Generic;

namespace RnAds.Model.Model
{
    public class Ad
    {
        public Guid Id { get; set; }

        /// <summary>
        /// название объявления
        /// </summary>
        public string NameAd { get; set; }

        /// <summary>
        /// регион
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// описание обьявления
        /// </summary>
        public string Comment { get; set; }

        public double Price { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// тип объявления
        /// </summary>
        public AdType TypeAd { get; set; }


        /// <summary>
        /// фотографии объявления
        /// </summary>
        public List<Picture> Pictures { get; set; } = new List<Picture>();

        /// <summary>
        /// список id пользователей,которые добавили это объявление в избранные
        /// </summary>
        public List<FavoriteUserId> ListUserId { get; set; } = new List<FavoriteUserId>();

        public ProductAttribute ProductAttribute { get; set; }

    }

    /// <summary>
    /// типы обьявлений
    /// </summary>
    public enum AdType
    {
        AdByAnimals,
        AdByBusinessAndEquipment,
        AdByElectronics,//+
        AdByForHomeAndGarden,
        AdByHobbiesAndRecreation,
        AdByPersonalItems,
        AdByRealty,
        AdByResume,
        AdByService,
        AdByTransport,
        AdByWork,
        AdBySparePartsAndAcsessories

    }
}

using System;
using System.Collections.Generic;

namespace RnAds.Model.Dto
{
    /// <summary>
    /// объявление в ленте поиска
    /// </summary>
    public class DtoPartialAd
    {
        public DtoPartialAd()
        {
            Pictures = new List<string>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// название объявления
        /// </summary>
        public string NameAdvertisement { get; set; }
        /// <summary>
        /// страна
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

        /// <summary>
        /// тип объявления
        /// </summary>
        public string TypeAd { get; set; }

        /// <summary>
        /// добавлено ли в избранные
        /// </summary>
        public bool IsFavorite { get; set; }

        public double Price { get; set; }

        /// <summary>
        ///загружаемые на сервер фотографии 
        /// </summary>
        //public List<IFormFile> ListPhotos { get; set; }

        /// <summary>
        /// фотографии объявления
        /// </summary>
        public List<string> Pictures { get; set; }
    }
}

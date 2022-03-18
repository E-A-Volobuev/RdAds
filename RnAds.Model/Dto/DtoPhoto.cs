using Microsoft.AspNetCore.Http;
using System;

namespace RnAds.Model.Dto
{
    public class DtoPhoto
    {
        /// <summary>
        /// фото ,загружаемые пользователем
        /// </summary>
        public IFormFile[] photo { get; set; }
        /// <summary>
        /// id объявления
        /// </summary>
        public Guid AdId { get; set; }

    }
}

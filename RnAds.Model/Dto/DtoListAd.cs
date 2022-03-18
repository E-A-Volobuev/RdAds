using System.Collections.Generic;

namespace RnAds.Model.Dto
{
    public class DtoListAd
    {
        /// <summary>
        /// список из 6 объявлений  (при пагинации мы выводим 6 объявлений на страницу)
        /// </summary>
        public List<DtoPartialAd> ListAd { get; set; } = new List<DtoPartialAd>();

        /// <summary>
        /// общее количество всех объявлений
        /// </summary>
        public int CountAds { get; set; }
    }
}

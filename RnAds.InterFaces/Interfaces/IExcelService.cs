
using System.Collections.Generic;

namespace RnAds.Core.Interfaces
{
    public interface IExcelService
    {
        /// <summary>
        /// получение названий регионов или городов из эксель документа в templates
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<string> GetNamesRegionsOrCities(string path);
    }
}

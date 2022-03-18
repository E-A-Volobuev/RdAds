using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RnAds.WEB.Controllers
{
    public class AdController : Controller
    {
        private readonly IAdService _adService;
        private readonly IExcelService _excelService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IAuthService _iAuthService;

        public AdController(IAdService adService, IExcelService excelService, IWebHostEnvironment appEnvironment, IAuthService iAuthService)
        {
            _adService = adService;
            _excelService = excelService;
            _appEnvironment = appEnvironment;
            _iAuthService = iAuthService;

        }

        /// <summary>
        /// получение объявления по ID +++++
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAdById(Guid id)
        {
            try
            {
                var dto = await _adService.GetByIdAdAsync(id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// вывод всех обьявлений определенной категории++++
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetAdByType(int page,AdQueryRequest adQuery)
        {
            try
            {
                var list= await _adService.GetListAdPartialAsync(adQuery, Guid.Empty);

                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list, page);

                    DtoListAd listAd = new DtoListAd();
                    listAd.ListAd = results;
                    listAd.CountAds = list.Count;

                    return Ok(listAd);
                }
                else
                    return BadRequest("Нет объявлений");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// вывод всех обьявлений определенной категории для авторизованных пользователей
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetAdByTypeByAuthorizeUsers(int page,AdQueryRequest adQuery)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);
                var list= await _adService.GetListAdPartialAsync(adQuery, userId);
                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list, page);

                    DtoListAd listAd = new DtoListAd();
                    listAd.ListAd = results;
                    listAd.CountAds = list.Count;

                    return Ok(listAd);
                }
                else
                    return BadRequest("Нет объявлений");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// создание объявления ++++
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<ActionResult> CreateAd([FromBody] DtoAd dto)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);
                if (dto != null)
                {
                    Guid id = await _adService.CreateAdAsync(dto, userId);

                    return Ok(id);
                }
                else
                    return BadRequest("Ошибка создания объявления");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// редактирование объявления++++
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateAds([FromBody] DtoAd dto)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                if (dto != null)
                {
                    await _adService.UpdateAdAsync(dto, userId);

                    return Ok(dto.Id);
                }
                else
                    return BadRequest("Ошибка обновления объявления");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// удаление объявления +++
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteAd([FromBody] DtoAd dto)
        {
            try
            {
                if (dto != null)
                {
                    await _adService.DeleteAdAsync(dto);
                    return Ok("Объявление успешно удалено");
                }
                else
                    return BadRequest("Ошибка удаления объявления");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение всех объявлений , независимо от категории ++++
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetAllAd(int page)
        { 
            try
            {
                var list = await _adService.GetListAllAdPartialAsync(Guid.Empty);

                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list,page);

                    DtoListAd listAd = new DtoListAd();
                    listAd.ListAd = results;
                    listAd.CountAds = list.Count;

                    return Ok(listAd);
                }

                else
                    return BadRequest("Нет объявлений");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// получение всех объявлений , независимо от категории для авторизованных пользователей
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetAllAdByAuthorizeUsers(int page)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                var list = await _adService.GetListAllAdPartialAsync(userId);

                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list, page);

                    DtoListAd listAd = new DtoListAd();
                    listAd.ListAd = results;
                    listAd.CountAds = list.Count;

                    return Ok(listAd);
                }
                else
                    return BadRequest("Нет объявлений");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// получение названий регионов и городов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetAllRegionOrCityName()
        {
            try
            {
                string tempFile = Path.Combine(_appEnvironment.ContentRootPath, "Templates\\города и регионы.xlsx");

                var list = await Task.Run(() => _excelService.GetNamesRegionsOrCities(tempFile));

                if (list != null)
                    return Ok(list);
                else
                    return BadRequest("ошибка получения названий регионов или городов");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// получение количества объявлений по категоряим
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAdsCount(string city)
        {
            try
            {
                if (city != null && city != string.Empty)
                {
                    var list = await _adService.GetAdsCount(city);

                    if (list != null)
                        return Ok(list);
                    else
                        return BadRequest("ошибка получения количества объявлений по категориям");
                }
                else
                    return BadRequest("Регион или город не выбран");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// хелпер частичной выдачи объявлений для пагинации (выдаем по 6 шт на страницу)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private List<DtoPartialAd> Helper(List<DtoPartialAd> list, int page)
        {
            List<DtoPartialAd> results = new List<DtoPartialAd>();

            //т.к. будем выводить по 6 объявлений на странице
            int num = page * 6;


            for (int i = 0; i < list.Count; i++)
            {
                if (i >= num - 6 && i < num)
                    results.Add(list[i]);
            }
            return results;
        }

    }
}

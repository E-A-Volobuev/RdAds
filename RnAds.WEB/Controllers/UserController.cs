using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RnAds.WEB.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserAdService _userService;
        private readonly IAuthService _iAuthService;

        public UserController(IUserAdService userService, IAuthService iAuthService)
        {
            _userService = userService;
            _iAuthService = iAuthService;
        }


        /// <summary>
        /// получение города по ip пользователя ++++
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetCity()
        {
            try
            {
                string city = _userService.GetResponceJsonByCity();
                return Json(city);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение объявлений текущего пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetCurrentUserAd(int page)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                var list = await _userService.GetAllAdByUser(userId);

                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list, page);

                    return Ok(results);
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
        /// добавление объявления в избранные
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> AddFavoriteAd(Guid id)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                if (id != Guid.Empty)
                {
                    await _userService.AddFavorite(userId, id);
                    return Ok();
                }
                else
                    return BadRequest("ошибка добавления объявления в избранные");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// удаление объявления из избранных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteFavoriteAd(Guid id)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                await _userService.DeleteFavorite(userId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение всех избранных объявлений пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<DtoPartialAd>>> GetAllFavoriteAd(int page)
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);

                var list = await _userService.GetAllFavorite(userId);

                if (list != null)
                {
                    List<DtoPartialAd> results = Helper(list, page);

                    return Ok(results);
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
        /// хелпер частичной выдачи объявлений для пагинации (выдаем по 10 шт на страницу)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private List<DtoPartialAd> Helper(List<DtoPartialAd> list, int page)
        {
            List<DtoPartialAd> results = new List<DtoPartialAd>();

            //т.к. будем выводить по 10 объявлений на странице
            int num = page * 10;

            for (int i = 0; i < list.Count; i++)
            {
                if (i >= num - 10 && i < num)
                    results.Add(list[i]);
            }
            return results;
        }

    }
}

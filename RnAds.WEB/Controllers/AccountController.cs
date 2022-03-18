using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RnAds.Core.Interfaces;
using RnAds.InterFaces.Interfaces;
using RnAds.Model.Dto;
using System;
using System.Threading.Tasks;

namespace RnAds.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _iAuthService;
        private readonly IAvatarService _iAvatarService;

        public AccountController(IAuthService iAuthService, IAvatarService iAvatarService)
        {
            _iAuthService = iAuthService;
            _iAvatarService = iAvatarService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if (model != null)
                {
                    var user = await _iAuthService.CreateAccountAsync(model);
                    if (user != null)
                        return Ok(user.Id);
                    else
                        return BadRequest("пользователь с таким именем уже существует");
                }
                return BadRequest("форма регистрации пустая");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAvatarUsers([FromForm] Guid id, [FromForm] IFormFile photo)
        {
            try
            {
                await _iAvatarService.AddAvatarAsync(id, photo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Token(string username, string password)
        {
            var identity = await _iAuthService.GetIdentityAsync(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var encodedJwt = _iAuthService.EncodedJwtToken(identity);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        /// <summary>
        /// получение аватара текущего пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAvatar()
        {
            try
            {
                var login = User.Identity.Name;
                var userId = await _iAuthService.GetCurrentUserIdAsync(login);
                string filePath = await _iAvatarService.GetAvatarAsync(userId);

                if (filePath == null)
                    return NotFound();

                var image = System.IO.File.OpenRead(filePath);

                return File(image, "image/jpeg");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение аватара владельца объявления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetAvatarOwner(Guid id)
        {
            try
            {
                string filePath = await _iAvatarService.GetAvatarAsync(id);

                if (filePath == null)
                    return NotFound();

                var image = System.IO.File.OpenRead(filePath);

                return File(image, "image/jpeg");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение текущего пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                var login = User.Identity.Name;
                var user = await _iAuthService.GetCurrentUser(login);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// редактирование данных текущего пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> EditCurrentUser([FromBody] DtoUser dto)
        {
            try
            {

                if (dto != null)
                {
                    await _iAuthService.EditCurrentUser(dto);
                    return Ok(dto.Id);
                }
                else
                    return BadRequest("Ошибка обновления профиля пользователя");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RnAds.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace RnAds.WEB.Controllers
{
    public class PictureController : Controller
    {
        private readonly IPictureService _iPictureService;
        public PictureController(IPictureService iPictureService)
        {
            _iPictureService = iPictureService;
        }
        /// <summary>
        /// получение фотографии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetPicture(Guid id)
        {
            try
            {
                string filePath = await _iPictureService.GetPhotoAsync(id);

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
        /// добавление фотографии
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddedPhoto([FromForm] Guid id, [FromForm] IFormFile[] photo)
        {
            try
            {
                await _iPictureService.AddPhotoAsync(id, photo,false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// обновление фотографии
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdatedPhoto([FromForm] Guid id, [FromForm] Guid [] picturesId, [FromForm] IFormFile[] photo)
        {
            try
            {
                await _iPictureService.UpdatePhotoAsync(id, picturesId, photo,false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

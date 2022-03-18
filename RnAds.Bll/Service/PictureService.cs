using Microsoft.AspNetCore.Http;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _iPictureRepository;
        public PictureService(IPictureRepository iPictureRepository)
        {
            _iPictureRepository = iPictureRepository;
        }

        /// <summary>
        /// добавление фотографий
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        public async Task AddPhotoAsync(Guid id, IFormFile[] photo,bool isMessage)
        {
            await CreatePhotoHelperAsync(id, photo, isMessage);
        }
        /// <summary>
        /// получение фотографии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetPhotoAsync(Guid id)
        {
            bool flag = await _iPictureRepository.ExistsAsync(id);
            if (flag)
            {
                var picture = await _iPictureRepository.GetByIdAsync(id);

                string filePath = picture.FilePath;

                return filePath;
            }
            return null;
        }

        /// <summary>
        /// обновление фотографий объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="picturesId"></param>
        /// <param name="photo"></param>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        public async Task UpdatePhotoAsync(Guid id, Guid[] picturesId, IFormFile[] photo, bool isMessage)
        {
            if (picturesId.Length > 0)
            {
                await _iPictureRepository.DeleteAllPhotoByIdAdAsync(id,picturesId);
            }

            await CreatePhotoHelperAsync(id,photo,isMessage);
        }
        /// <summary>
        /// хелпер создания фотографий объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <param name="isMessage"></param>
        /// <returns></returns>
        private async Task CreatePhotoHelperAsync(Guid id, IFormFile[] photo, bool isMessage)
        {
            if (photo.Length != 0)
            {
                foreach (var file in photo)
                {
                    // путь к временной папке
                    string path = Path.GetTempPath() + $"{file.Name}";
                    // сохраняем файл во временную папку 
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    string newPath = Path.Combine(@"D:\хранилище\") + file.FileName;
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        fileInf.CopyTo(newPath, true);
                    }

                    Picture pic = new Picture();

                    if (isMessage)
                        pic.MessageId = id;
                    else
                        pic.AdId = id;

                        pic.FilePath = newPath;

                    await _iPictureRepository.CreateAsync(pic);

                }
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using RnAds.Core.Interfaces;
using RnAds.Model.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class AvatarService : IAvatarService
    {
        private readonly IAvatarRepository _iAvatarRepository;
        public AvatarService(IAvatarRepository iAvatarRepository)
        {
            _iAvatarRepository = iAvatarRepository;
        }
        /// <summary>
        /// добавление аватара пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public async Task AddAvatarAsync(Guid id, IFormFile photo)
        {

            // путь к временной папке
            string path = Path.GetTempPath() + $"{photo.Name}";
            // сохраняем файл во временную папку 
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }
            string newPath = Path.Combine(@"D:\аватары\") + photo.FileName;
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.CopyTo(newPath, true);
            }

            AvatarUser avatar = new AvatarUser();
            avatar.UserId = id.ToString();
            avatar.FilePath = newPath;

            await _iAvatarRepository.CreateAsync(avatar);

        }
        /// <summary>
        /// получение аватара пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetAvatarAsync(Guid id)
        {
            var avatar = await _iAvatarRepository.GetAvatarAsync(id);
            if (avatar != null)
                return avatar.FilePath;
            else
                return null;
        }
    }
}

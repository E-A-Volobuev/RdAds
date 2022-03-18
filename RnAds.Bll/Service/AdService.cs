using RnAds.Core.Interfaces;
using RnAds.Model.Dto;
using RnAds.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RnAds.Bll.Service
{
    public class AdService : IAdService
    {
        private readonly IAdRepository _iAdRepository;
        private readonly IFavoriteRepository _iFavoriteRepository;
        private readonly IProductAttributeRepository _iProductRepository;

        private string[] types = new string[] { "Животные", "Бизнес и оборудование для бизнеса", "Электроника", "Для дома и дачи", "Хобби и отдых", "Личные вещи", "Недвижимость", "Резюме",
                                                "Сервис", "Транспорт", "Работа", "Запасные части и аксессуары" };
        public AdService(IAdRepository iAdRepository, IProductAttributeRepository iProductRepository, IFavoriteRepository iFavoriteRepository)
        {
            _iAdRepository = iAdRepository;
            _iProductRepository = iProductRepository;
            _iFavoriteRepository = iFavoriteRepository;
        }

        /// <summary>
        /// получение записи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DtoAd> GetByIdAdAsync(Guid id)
        {
            var flagAd = await _iAdRepository.ExistsAsync(id);

            DtoAd dto = new DtoAd();

            if (flagAd == true)
            {
                Ad currentAd = new Ad();
                currentAd = await _iAdRepository.GetByIdAndPhotosAsync(id);

                dto.Id = currentAd.Id;
                dto.NameAd = currentAd.NameAd;
                dto.Owner = currentAd.User;
                dto.Country = currentAd.Country;
                dto.City = currentAd.City;
                dto.Comment = currentAd.Comment;
                dto.Price = currentAd.Price;

                for (int i = 0; i < types.Length; i++)
                {
                    int index = (int)currentAd.TypeAd;
                    dto.TypeAd = types[index];
                }
                dto.Pictures = currentAd.Pictures;

                ProductAttribute prod = new ProductAttribute();
                prod = await _iProductRepository.GetByIdAdAsync(currentAd);

                dto.TypeItem = prod.TypeItem;
                dto.CategoryItem = prod.CategoryItem;
                dto.Condition = prod.Condition;
                dto.Availability = prod.Availability;
                dto.Brand = prod.Brand;
                dto.Area = prod.Area;
                dto.NumberFloor = prod.NumberFloor;
                dto.CountRooms = prod.CountRooms;
                dto.Citizenship = prod.Citizenship;
                dto.SphereOfActivity = prod.SphereOfActivity;
                dto.WorkingSchedule = prod.WorkingSchedule;
                dto.WorkExperience = prod.WorkExperience;
                dto.Sex = prod.Sex;
                dto.FrequencyOfPayments = prod.FrequencyOfPayments;
                dto.YearReliase = prod.YearReliase;
                dto.Kilometers = prod.Kilometers;
                dto.OwnerCount = prod.OwnerCount;
                dto.EngineVolume = prod.EngineVolume;
                dto.HorsePower = prod.HorsePower;
                dto.TypeEngine = prod.TypeEngine;
                dto.TypeGearBox = prod.TypeGearBox;
                dto.TypeDrive = prod.TypeDrive;
                dto.TypeBody = prod.TypeBody;
                dto.Color = prod.Color;
                dto.TypeHelm = prod.TypeHelm;
                dto.TypeSparePartsAndAcsessories = prod.TypeSparePartsAndAcsessories;
            }

            return dto;

        }

        /// <summary>
        /// получение объявлений определенной категории
        /// </summary>
        /// <param name="adQuery"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DtoPartialAd>> GetListAdPartialAsync(AdQueryRequest adQuery, Guid userId)
        {
            string type = "";

            for (int i = 0; i < types.Length; i++)
            {
                if (adQuery.type == types[i])
                {
                    type = i.ToString();
                    adQuery.type = type;
                }
            }
            if (adQuery.type == "Любая категория")
                adQuery.type = null;
            if (adQuery.city == "Россия")
                adQuery.city = null;

            var list = await CreateDtoListHelper(await _iAdRepository.GetListAdByRegionByTypeAsync(adQuery), userId);
            return list;
        }

        /// <summary>
        /// хэлпер создания частичных обьявлений 
        /// </summary>
        /// <param name="listAd"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DtoPartialAd>> CreateDtoListHelper(List<Ad> listAd, Guid userId)
        {
            List<DtoPartialAd> dtoList = new List<DtoPartialAd>();

            if (listAd != null)
            {
                foreach (var s in listAd)
                {
                    DtoPartialAd partial = new DtoPartialAd();
                    partial.Id = s.Id;
                    partial.City = s.City;
                    partial.Comment = s.Comment;
                    partial.Country = s.Country;
                    partial.NameAdvertisement = s.NameAd;
                    partial.Price = s.Price;
                    partial.Pictures = s.Pictures.Select(e => e.Id.ToString()).ToList();

                    bool isFavorite = await _iFavoriteRepository.ExistByUserIdAndAdIdAsync(userId, s.Id);
                    if (isFavorite)
                        partial.IsFavorite = true;
                    else
                        partial.IsFavorite = false;

                    CheckTypeHelper(partial, s);


                    if (partial != null)
                        dtoList.Add(partial);
                }
                return dtoList;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// определение типа объявления
        /// </summary>
        /// <param name="partial"></param>
        /// <param name="ad"></param>
        private void CheckTypeHelper(DtoPartialAd partial, Ad ad)
        {
            for (int i = 0; i < types.Length; i++)
            {
                int index = (int)ad.TypeAd;
                partial.TypeAd = types[index];
            }
        }
        /// <summary>
        /// создание объявления 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAdAsync(DtoAd dto, Guid userId)
        {
            Ad ad = new Ad();
            CreateAdHelper(dto, userId, ad);

            await _iAdRepository.CreateAsync(ad);

            ProductAttribute prod = new ProductAttribute();
            CreateProductAttributeHelper(dto, ad, prod);

            await _iProductRepository.CreateAsync(prod);

            return ad.Id;
        }
        /// <summary>
        /// хелпер создания объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <param name="ad"></param>
        private void CreateAdHelper(DtoAd dto, Guid userId, Ad ad)
        {
            ad.NameAd = dto.NameAd;
            ad.Country = dto.Country;
            ad.City = dto.City;
            ad.Comment = dto.Comment;
            ad.Price = dto.Price;
            ad.TypeAd = TypeHelper(dto);
            ad.UserId = userId;
        }
        /// <summary>
        /// преобразование типа объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private AdType TypeHelper(DtoAd dto)
        {
            switch (dto.TypeAd)
            {
                case "Животные": return AdType.AdByAnimals;
                case "Бизнес и оборудование для бизнеса": return AdType.AdByBusinessAndEquipment;
                case "Электроника": return AdType.AdByElectronics;
                case "Для дома и дачи": return AdType.AdByForHomeAndGarden;
                case "Хобби и отдых": return AdType.AdByHobbiesAndRecreation;
                case "Личные вещи": return AdType.AdByPersonalItems;
                case "Недвижимость": return AdType.AdByRealty;
                case "Резюме": return AdType.AdByResume;
                case "Сервис": return AdType.AdByService;
                case "Запасные части и аксессуары": return AdType.AdBySparePartsAndAcsessories;
                case "Транспорт": return AdType.AdByTransport;

                default: return AdType.AdByWork;
            }
        }

        /// <summary>
        /// хелпер создания аттрибутов объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ad"></param>
        /// <param name="prod"></param>
        private void CreateProductAttributeHelper(DtoAd dto, Ad ad, ProductAttribute prod)
        {
            prod.TypeItem = dto.TypeItem;
            prod.CategoryItem = dto.CategoryItem;
            prod.Condition = dto.Condition;
            prod.Availability = dto.Availability;
            prod.Brand = dto.Brand;
            prod.Area = dto.Area;
            prod.NumberFloor = dto.NumberFloor;
            prod.CountRooms = dto.CountRooms;
            prod.Citizenship = dto.Citizenship;
            prod.SphereOfActivity = dto.SphereOfActivity;
            prod.WorkingSchedule = dto.WorkingSchedule;
            prod.WorkExperience = dto.WorkExperience;
            prod.Sex = dto.Sex;
            prod.FrequencyOfPayments = dto.FrequencyOfPayments;
            prod.YearReliase = dto.YearReliase;
            prod.Kilometers = dto.Kilometers;
            prod.OwnerCount = dto.OwnerCount;
            prod.EngineVolume = dto.EngineVolume;
            prod.HorsePower = dto.HorsePower;
            prod.TypeEngine = dto.TypeEngine;
            prod.TypeGearBox = dto.TypeGearBox;
            prod.TypeDrive = dto.TypeDrive;
            prod.TypeBody = dto.TypeBody;
            prod.Color = dto.Color;
            prod.TypeHelm = dto.TypeHelm;
            prod.TypeSparePartsAndAcsessories = dto.TypeSparePartsAndAcsessories;
            prod.Ad = ad;

        }

        /// <summary>
        /// обновление объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task UpdateAdAsync(DtoAd dto, Guid userId)
        {
            var currentAd = await _iAdRepository.GetByIdAndPhotosAsync(dto.Id);
            CreateAdHelper(dto, userId, currentAd);

            await _iAdRepository.UpdateAsync(currentAd);

            var productAttribute = await _iProductRepository.GetByIdAdAsync(currentAd);
            CreateProductAttributeHelper(dto, currentAd, productAttribute);

            await _iProductRepository.UpdateAsync(productAttribute);

        }

        /// <summary>
        /// удаление объявления
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task DeleteAdAsync(DtoAd dto)
        {
            bool flag = await _iAdRepository.ExistsAsync(dto.Id);

            if (flag)
            {
                var currentAd = await _iAdRepository.GetByIdAsync(dto.Id);
                await _iAdRepository.DeleteAsync(currentAd);
            }
        }

        /// <summary>
        /// получение всех объявлений
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DtoPartialAd>> GetListAllAdPartialAsync(Guid userId)
        {

            var listAd = await _iAdRepository.GetAllAdAndPhotosAsync();
            var dtoList = await CreateDtoListHelper(listAd, userId);

            return dtoList;
        }

        /// <summary>
        /// получение количества объявлений по категориям
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public async Task<List<int>> GetAdsCount(string city)
        {
            List<int> listCount = new List<int>();

            List<Ad> listAd = new List<Ad>();

            AdQueryRequest request = new AdQueryRequest() { city = city };

            if (city == "Россия")
                listAd = await _iAdRepository.GetAllAdAndPhotosAsync();
            else
                listAd = await _iAdRepository.GetListAdByRegionByTypeAsync(request);

            if (listAd != null)
            {
                int animalCount = listAd.Where(x => x.TypeAd == AdType.AdByAnimals).Count();
                int businessAndEquipmentCount = listAd.Where(x => x.TypeAd == AdType.AdByBusinessAndEquipment).Count();
                int electronicsCount = listAd.Where(x => x.TypeAd == AdType.AdByElectronics).Count();
                int homeAndGardenCount = listAd.Where(x => x.TypeAd == AdType.AdByForHomeAndGarden).Count();
                int hobbiesAndRecreationCount = listAd.Where(x => x.TypeAd == AdType.AdByHobbiesAndRecreation).Count();
                int itemsCount = listAd.Where(x => x.TypeAd == AdType.AdByPersonalItems).Count();
                int realtyCount = listAd.Where(x => x.TypeAd == AdType.AdByRealty).Count();
                int resumeCount = listAd.Where(x => x.TypeAd == AdType.AdByResume).Count();
                int serviceCount = listAd.Where(x => x.TypeAd == AdType.AdByService).Count();
                int transportCount = listAd.Where(x => x.TypeAd == AdType.AdByTransport).Count();
                int workCount = listAd.Where(x => x.TypeAd == AdType.AdByWork).Count();
                int sparePartsAndAcsessoriesCount = listAd.Where(x => x.TypeAd == AdType.AdBySparePartsAndAcsessories).Count();

                listCount.Add(animalCount);
                listCount.Add(businessAndEquipmentCount);
                listCount.Add(electronicsCount);
                listCount.Add(homeAndGardenCount);
                listCount.Add(hobbiesAndRecreationCount);
                listCount.Add(itemsCount);
                listCount.Add(realtyCount);
                listCount.Add(resumeCount);
                listCount.Add(serviceCount);
                listCount.Add(transportCount);
                listCount.Add(workCount);
                listCount.Add(sparePartsAndAcsessoriesCount);
            }

            return listCount;
        }


    }
}

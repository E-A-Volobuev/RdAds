using System;

namespace RnAds.Model.Model
{
    public class ProductAttribute
    {
        public Guid Id { get; set; }

        /// <summary>
        /// вид бизнеса или товара(услуги)
        /// </summary>
        public string TypeItem { get; set; }

        /// <summary>
        /// категория бизнеса или товара(услуги)
        /// </summary>
        public string CategoryItem { get; set; }

        /// <summary>
        /// состояние
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// наличие
        /// </summary>
        public string Availability { get; set; }

        /// <summary>
        /// производитель
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// площадь
        /// </summary>
        public int Area { get; set; }

        /// <summary>
        /// этаж
        /// </summary>
        public int NumberFloor { get; set; }

        /// <summary>
        /// количество комнат
        /// </summary>
        public int CountRooms { get; set; }

        /// <summary>
        /// гражданство
        /// </summary>
        public string Citizenship { get; set; }
        /// <summary>
        /// сфера деятельности
        /// </summary>
        public string SphereOfActivity { get; set; }
        /// <summary>
        /// график работы
        /// </summary>
        public string WorkingSchedule { get; set; }
        /// <summary>
        /// пол
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// опыт работы
        /// </summary>
        public string WorkExperience { get; set; }

        /// <summary>
        /// график выплаты
        /// </summary>
        public string FrequencyOfPayments { get; set; }

        /// <summary>
        /// год выпуска
        /// </summary>
        public int YearReliase { get; set; }

        /// <summary>
        /// пробег
        /// </summary>
        public int Kilometers { get; set; }

        /// <summary>
        /// количество владельцев
        /// </summary>
        public int OwnerCount { get; set; }

        /// <summary>
        /// обьем двигателя
        /// </summary>
        public double EngineVolume { get; set; }

        /// <summary>
        /// л.с.
        /// </summary>
        public int HorsePower { get; set; }

        /// <summary>
        /// тип двигателя (бензин/дизель)
        /// </summary>
        public string TypeEngine { get; set; }

        /// <summary>
        /// механика/автомат
        /// </summary>
        public string TypeGearBox { get; set; }

        /// <summary>
        /// привод (передний/задний)
        /// </summary>
        public string TypeDrive { get; set; }

        /// <summary>
        /// тип кузова
        /// </summary>
        public string TypeBody { get; set; }

        /// <summary>
        /// цвет
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// руль (правый/левый)
        /// </summary>
        public string TypeHelm { get; set; }

        /// <summary>
        /// тип запчасти
        /// </summary>
        public string TypeSparePartsAndAcsessories { get; set; }

        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
    }
}

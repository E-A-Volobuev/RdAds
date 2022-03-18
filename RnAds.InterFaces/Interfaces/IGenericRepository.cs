using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RnAds.InterFaces.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>Получение записи по Id</summary>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>Создание записи</summary>
        Task CreateAsync(T item);

        /// <summary>Обновление записи</summary>
        Task UpdateAsync(T item);

        /// <summary> Запись существует, проверка по Id </summary>
        Task<bool> ExistsAsync(Guid id);

        //удаление объекта
        Task DeleteAsync(T item);

        Task DeleteRangeAsync(List<T> item);

    }
}

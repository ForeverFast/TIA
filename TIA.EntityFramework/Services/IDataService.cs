using System;
using System.Threading.Tasks;

namespace TIA.EntityFramework.Services
{
    public interface IDataService<T>
        where T : class
    {
        /// <summary>
        /// Получение объекта по Id
        /// </summary>
        /// <param name="guid"> Идентификатор объекта </param>
        /// <returns></returns>
        Task<T> GetById(Guid guid);

        /// <summary>
        /// Добавление объекта в БД
        /// </summary>
        /// <param name="entity"> Данные нового объекта </param>
        /// <returns> Сохранённый объект в бд </returns>
        Task<T> Add(T entity);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="entity"> Данные для обновление </param>
        /// <param name="guid"> Идентификатор обновляемого объекта </param>
        /// <returns> Обновлённый объект </returns>
        Task<T> Update(T entity, Guid guid);

        /// <summary>
        /// Удаление объекта по Id
        /// </summary>
        /// <param name="guid"> Id удаляемого объекта </param>
        /// <returns></returns>
        Task<bool> Delete(Guid guid);
    }
}

using ApricodeApplicantWork.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Infrastructure.Repository
{
    /// <summary>
    /// Обобщенный CRUD интерфейс для дальнейше реализации репозиториями
    /// </summary>
    /// <typeparam name="T">Сущность репозитория</typeparam>
    public abstract class RepositoryAbstraction<T> where T : BaseEntry
    {

        public ApplicationContext _genericContext;

        /// <summary>
        /// Добавить сущеость T в базу данных
        /// </summary>
        public virtual async Task AddAsync(T entity)
        {
            _genericContext.Set<T>().Add(entity);
            await _genericContext.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить сущность в базе данных
        /// </summary>
        public virtual async Task UpdateAsync(T entity)
        {
            _genericContext.Set<T>().Update(entity);
            await _genericContext.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить сущность из базы данных
        /// </summary>
        public virtual async Task DeleteAsync(T entity)
        {
            _genericContext.Set<T>().Remove(entity);
            await _genericContext.SaveChangesAsync();
        }

        /// <summary>
        /// Получить сущность из базы данных по индификатору
        /// </summary>
        public virtual async Task<T> GetAsync(long indificator) => await _genericContext.Set<T>().Where(m => m.Id == indificator).FirstOrDefaultAsync();
    }
}
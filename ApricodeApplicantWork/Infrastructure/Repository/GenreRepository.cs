using ApricodeApplicantWork.Models;
using System;

namespace ApricodeApplicantWork.Infrastructure.Repository
{
    /// <summary>
    /// Абстрактный уровень для работы с сущностью "Genre"
    /// </summary>
    public class GenreRepository : RepositoryAbstraction<Genre>
    {
        /// <summary>
        /// Конструктор для класса GenreRepository
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public GenreRepository(ApplicationContext context)
        {
            _genericContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

using ApricodeApplicantWork.Models;
using System;

namespace ApricodeApplicantWork.Infrastructure.Repository
{
    /// <summary>
    /// Абстрактный уровень для работы с сущностью "Studio"
    /// </summary>
    public class StudioRepository : RepositoryAbstraction<Studio>
    {
        /// <summary>
        /// Конструктор для класса StudioRepository
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public StudioRepository(ApplicationContext context)
        {
            _genericContext = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

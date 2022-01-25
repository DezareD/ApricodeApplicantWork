using ApricodeApplicantWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Infrastructure.Repository
{
    public interface IGameRepository
    {
        /// <summary>
        /// Получить все сущности Game
        /// </summary>
        /// <returns>Список всех сущностей Game</returns>
        Task<List<Game>> GetAllAsync();
    }

    /// <summary>
    /// Абстрактный уровень для работы с сущностью "Game"
    /// </summary>
    public class GameRepository : RepositoryAbstraction<Game>, IGameRepository
    {
        /// <summary>
        /// Конструктор для класса GapeRepository
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public GameRepository(ApplicationContext context)
        {
            _genericContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Game>> GetAllAsync() => await _genericContext.Games.ToListAsync();
    }
}

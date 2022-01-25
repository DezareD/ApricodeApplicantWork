using ApricodeApplicantWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Infrastructure.Repository
{
    public interface IGameGenreRepository
    {
        /// <summary>
        /// Получить все GameGenre сущности в соответствии с индификатором игры
        /// </summary>
        /// <param name="gameIndificator">Индификатор сущности Game</param>
        /// <returns>Список из сущностей GameGenre</returns>
        Task<List<GameGenre>> GetByGameId(long gameIndificator);

        /// <summary>
        /// Удалеяет все вхождения по поиску
        /// </summary>
        Task DeleteByGameIdAndGenreId(long gameId, long genreId);

        /// <summary>
        /// Получить все GameGenre сущности в соответствии с индификатором жанра
        /// </summary>
        Task<List<GameGenre>> GetByGenreId(long genreIndificator);
    }

    /// <summary>
    /// Абстрактный уровень для работы с сущностью "GameGenre"
    /// </summary>
    public class GameGenreRepository : RepositoryAbstraction<GameGenre>, IGameGenreRepository
    {
        /// <summary>
        /// Конструктор для класса GameGenreRepository
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public GameGenreRepository(ApplicationContext context)
        {
            _genericContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task DeleteByGameIdAndGenreId(long gameId, long genreId)
        {
            var searchList = await _genericContext.GameGenres.Where(m => m.GameId == gameId).Where(m => m.GenreId == genreId).ToListAsync();
            _genericContext.GameGenres.RemoveRange(searchList);
            await _genericContext.SaveChangesAsync();
        }
        public async Task<List<GameGenre>> GetByGameId(long gameIndificator) => await _genericContext.GameGenres.Where(m => m.GameId == gameIndificator).ToListAsync();
        public async Task<List<GameGenre>> GetByGenreId(long genreIndificator) => await _genericContext.GameGenres.Where(m => m.GenreId == genreIndificator).ToListAsync();
    }
}

using ApricodeApplicantWork.Dto;
using ApricodeApplicantWork.Infrastructure.Repository;
using ApricodeApplicantWork.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Infrastructure.Services
{
    public interface IApplicationServices
    {
        /// <summary>
        /// Получить все игры из базы данных с сопутствующими данныи
        /// </summary>
        Task<List<GameModel>> GetAllGames();

        /// <summary>
        /// Получить все жанры для игры
        /// </summary>
        Task<List<Genre>> GetAllGenreByGameId(long gameId);

        /// <summary>
        /// Получить единичный экземпляр сущности игры
        /// </summary>
        Task<GameModel> GetGameModel(long gameId);

        /// <summary>
        /// Получить чистую сущность game с базы данных
        /// </summary>
        Task<Game> GetGame(long gameId);

        /// <summary>
        /// Удалить сущность game
        /// </summary>
        Task DeleteGame(Game game);

        /// <summary>
        /// Обновляет сущнсть Game и все сопутствующие
        /// </summary>
        Task UpdateGame(UpdateGameModel model);

        /// <summary>
        /// Добавить игру и сопутствующие сущности
        /// </summary>
        Task AddGame(AddGameModel model);

        Task<List<GameModel>> GetGameListByGenreId(long genreId);
    }

    public class ApplicationServices : IApplicationServices
    {
        private readonly GameRepository _gameRepository;
        private readonly StudioRepository _studioRepository;
        private readonly GameGenreRepository _gameGenreRepository;
        private readonly GenreRepository _genreRepository;

        public ApplicationServices(IGameRepository gameRepository, RepositoryAbstraction<Studio> studioRepository,
            IGameGenreRepository gameGenreRepository, RepositoryAbstraction<Genre> genreRepository)
        {
            _gameRepository = (GameRepository)(gameRepository ?? throw new ArgumentNullException(nameof(gameRepository)));
            _studioRepository = (StudioRepository)(studioRepository ?? throw new ArgumentNullException(nameof(studioRepository)));
            _gameGenreRepository = (GameGenreRepository)(gameGenreRepository ?? throw new ArgumentNullException(nameof(gameGenreRepository)));
            _genreRepository = (GenreRepository)(genreRepository ?? throw new ArgumentNullException(nameof(genreRepository)));
        }

        public async Task DeleteGame(Game game) => await _gameRepository.DeleteAsync(game);
        public async Task<Game> GetGame(long gameId) => await _gameRepository.GetAsync(gameId);

        public async Task<List<GameModel>> GetGameListByGenreId(long genreId)
        {
            var genre = await _genreRepository.GetAsync(genreId);
            var retList = new List<GameModel>();

            if(genre != null)
            {
                var list = await _gameGenreRepository.GetByGenreId(genreId);

                foreach(var gg in list)
                {
                    var searchGame = await _gameRepository.GetAsync(gg.GameId);

                    if(searchGame != null)
                    {
                        var enchantedModel = await GetGameModel(searchGame.Id);

                        retList.Add(enchantedModel);
                    }
                }
            }

            return retList;
        }

        public async Task AddGame(AddGameModel model)
        {
            var game = new Game();

            game.Name = model.Name;

            var company = await _studioRepository.GetAsync(model.CompanyId);
            if (company != null)
                game.CompanyId = model.CompanyId;

            await _gameRepository.AddAsync(game);

            if (model.GenreIds != null)
            {
                foreach (var newGenreId in model.GenreIds)
                {
                    var search = await _genreRepository.GetAsync(newGenreId);

                    if (search != null)
                        await _gameGenreRepository.AddAsync(new GameGenre()
                        {
                            GameId = game.Id,
                            GenreId = search.Id
                        });
                }
            }
        }

        public async Task UpdateGame(UpdateGameModel model)
        {
            var game = await _gameRepository.GetAsync(model.Id);

            if(game != null)
            {
                var company = await _studioRepository.GetAsync(model.CompanyId);

                if(company != null)
                    game.CompanyId = model.CompanyId;

                var deleteList = await GetAllGenreByGameId(game.Id);

                // очищаем все жанры

                foreach (var genre in deleteList)
                    await _gameGenreRepository.DeleteByGameIdAndGenreId(game.Id, genre.Id);

                // добавяем новые жарны

                if (model.GenreIds != null)
                {
                    foreach (var newGenreId in model.GenreIds)
                    {
                        var search = await _genreRepository.GetAsync(newGenreId);

                        if (search != null)
                            await _gameGenreRepository.AddAsync(new GameGenre()
                            {
                                GameId = game.Id,
                                GenreId = search.Id
                            });
                    }
                }

                game.Name = model.Name;

                await _gameRepository.UpdateAsync(game);
            }
        }

        public async Task<GameModel> GetGameModel(long gameId)
        {
            var game = await _gameRepository.GetAsync(gameId);

            if (game != null)
            {
                var company = await _studioRepository.GetAsync(game.CompanyId);
                var genreList = await GetAllGenreByGameId(game.Id);

                return new GameModel()
                {
                    Name = game.Name,
                    Genres = genreList,
                    Studio = company,
                    Id = game.Id
                };
            }
            else return null;
        }

        public async Task<List<GameModel>> GetAllGames()
        {
            var allGames = await _gameRepository.GetAllAsync();
            var retList = new List<GameModel>();

            foreach(var game in allGames)
            {
                var company = await _studioRepository.GetAsync(game.CompanyId);
                var genreList = await GetAllGenreByGameId(game.Id);

                retList.Add(new GameModel()
                {
                    Genres = genreList,
                    Name = game.Name,
                    Studio = company,
                    Id = game.Id
                });
            }

            return retList;
        }

        public async Task<List<Genre>> GetAllGenreByGameId(long gameId)
        {
            var allGenreGame = await _gameGenreRepository.GetByGameId(gameId);
            var retList = new List<Genre>();

            foreach(var genre in allGenreGame)
            {
                var find = await _genreRepository.GetAsync(genre.GenreId);
                if(find != null)
                    retList.Add(find);
            }

            return retList;
        }
    }
}

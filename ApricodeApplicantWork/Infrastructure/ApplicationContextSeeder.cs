using ApricodeApplicantWork.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApricodeApplicantWork.Infrastructure
{
    /// <summary>
    /// Генератор первоначальных данных и автоматическая миграция.
    /// </summary>
    public class ApplicationContextSeeder
    {
        public static List<Studio> _studios = new List<Studio>()
        {
            new Studio() { Name = "Rockstar North", Information = "Is a British video game studio founded by David Jones in Dundee and based in Leith Street, Edinburgh." },
            new Studio() { Name = "CD Projekt RED", Information = "A computer game developer based in Poland. a significant subsidiary of CD Projekt." }
        };

        public static List<Game> _games = new List<Game>()
        {
            new Game() { Name = "Grand Thef Auto V", CompanyId = 1 },
            new Game() { Name = "Red Dead Redemption 2", CompanyId = 1 },
            new Game() { Name = "Cyberpunk 2077", CompanyId = 2 }
        };

        public static List<Genre> _genres = new List<Genre>()
        {
            new Genre() { Name = "Shooter games", Information = "In shooter games (or simply shooters), players use ranged weapons to participate in the action, which takes place at a distance." },
            new Genre() { Name = "Fighting games", Information = "Fighting games center around close-ranged combat, typically one-on-one fights or against a small number of equally powerful opponents, often involving violent and exaggerated unarmed attacks." },
            new Genre() { Name = "Survival games", Information = "Survival games start the player off with minimal resources, in a hostile, open-world environment, and require them to collect resources, craft tools, weapons, and shelter, in order to survive as long as possible." },
            new Genre() { Name = "Role-playing", Information = "Role-playing video games draw their gameplay from traditional tabletop role-playing games like Dungeons & Dragons." },
            new Genre() { Name = "Action-adventure", Information = "Although Action-adventure games can divide into action or adventure games, they combine elements of their two component genres, typically featuring long-term obstacles that must be overcome using a tool or item as leverage (which is collected earlier), as well as many smaller obstacles almost constantly in the way, that require elements of action games to overcome." },
            new Genre() { Name = "Simulation", Information = "Simulation video games is a diverse super-category of games, generally designed to closely simulate aspects of a real or fictional reality." }
        };

        public static List<GameGenre> _gameGenres = new List<GameGenre>()
        {
            new GameGenre() { GameId = 1, GenreId = 5 },
            new GameGenre() { GameId = 2, GenreId = 5 },
            new GameGenre() { GameId = 3, GenreId = 5 },
            new GameGenre() { GameId = 3, GenreId = 4 }
        };

        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var context = (ApplicationContext)applicationBuilder
                .ApplicationServices.GetService(typeof(ApplicationContext));

            using (context)
            {
                context.Database.Migrate();

                if(!context.Games.Any())
                    await context.Games.AddRangeAsync(_games);

                if (!context.GameGenres.Any())
                    await context.GameGenres.AddRangeAsync(_gameGenres);

                if (!context.Genres.Any())
                    await context.Genres.AddRangeAsync(_genres);

                if (!context.Studios.Any())
                    await context.Studios.AddRangeAsync(_studios);

                await context.SaveChangesAsync();
            }
        }
    }
}

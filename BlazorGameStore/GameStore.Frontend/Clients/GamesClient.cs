using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games = new()
    {
        new GameSummary { Id = 1, Name = "The Witcher 3: Wild Hunt", Genre = "RPG", Price = 29.99m, ReleaseDate = new DateOnly(2015, 5, 19) },
        new GameSummary { Id = 2, Name = "Grand Theft Auto V", Genre = "Action", Price = 29.99m, ReleaseDate = new DateOnly(2013, 9, 17) },
        new GameSummary { Id = 3, Name = "Red Dead Redemption 2", Genre = "Action", Price = 59.99m, ReleaseDate = new DateOnly(2018, 10, 26) },
        new GameSummary { Id = 4, Name = "The Elder Scrolls V: Skyrim", Genre = "RPG", Price = 19.99m, ReleaseDate = new DateOnly(2011, 11, 11) },
        new GameSummary { Id = 5, Name = "The Legend of Zelda: Breath of the Wild", Genre = "Action", Price = 59.99m, ReleaseDate = new DateOnly(2017, 3, 3) }
    };

    private readonly Genre[] genres = new GenresClient().GetGenres();
    public GameSummary[] GetGames() => games.ToArray();

    public void AddGame(GameDetails gameDetails)
    {
        Genre genre = GetGenreById(gameDetails.GenreId);

        var game = new GameSummary
        {
            Id = games.Count + 1,
            Name = gameDetails.Name,
            Genre = genre.Name,
            Price = gameDetails.Price,
            ReleaseDate = gameDetails.ReleaseDate
        };
        games.Add(game);
    }

    private Genre GetGenreById(string? id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var genre = genres.Single(x => x.Id == int.Parse(id));
        return genre;
    }

    public GameDetails GetGame(int id)
    {
        GameSummary game = GetGameSummaryById(id);

        var genre = genres.Single(x => string.Equals(x.Name, game.Genre, StringComparison.OrdinalIgnoreCase));
        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void UpdateGame(GameDetails gameDetails)
    {
        GameSummary game = GetGameSummaryById(gameDetails.Id);

        Genre genre = GetGenreById(gameDetails.GenreId);

        game.Name = gameDetails.Name;
        game.Genre = genre.Name;
        game.Price = gameDetails.Price;
        game.ReleaseDate = gameDetails.ReleaseDate;
    }

    private GameSummary GetGameSummaryById(int id)
    {
        var game = games.SingleOrDefault(x => x.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }
}

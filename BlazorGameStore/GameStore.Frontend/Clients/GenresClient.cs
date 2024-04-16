using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GenresClient
{
    private readonly List<Genre> genres = new()
    {
        new Genre { Id = 1, Name = "Action" },
        new Genre { Id = 2, Name = "RPG" }
    };
    public Genre[] GetGenres() => genres.ToArray();
}

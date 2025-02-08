using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate;

public class MovieGenre : Entity<MovieGenreId>
{
    public string Name { get; set; }

    private MovieGenre(MovieGenreId movieGenreId, string name) 
        : base(movieGenreId)
    {
        Name = name;
    }

    public static MovieGenre Create(string name)
    {
        return new MovieGenre(MovieGenreId.CreateUnique(), name);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence.Movie
{
    public interface IMovieGenreRepository
    {
        Task<IEnumerable<MovieGenre>> GetByMovieId(MovieId movieId);

        Task AddGenre(MovieId movieId, MovieGenre genre);

        Task UpdateGenre(MovieGenre genre);

        Task DeleteGenre(MovieId movieId, MovieGenreId genreId);
    }
} 
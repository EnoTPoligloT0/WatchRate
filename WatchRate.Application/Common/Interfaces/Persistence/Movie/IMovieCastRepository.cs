using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence.Movie;

public interface IMovieCastRepository
{
    Task<IEnumerable<MovieCast>> GetCastByMovieId(MovieId movieId);
    Task<IEnumerable<MovieCast>> GetById(MovieCastId castId);
    Task AddCast(MovieId movieId, MovieCast cast);
    Task UpdateCast(MovieId movieId, MovieCast cast);
    Task DeleteCast(MovieCast cast);
    Task DeleteCastByMovieId(MovieId movieId, MovieCastId castId);
}
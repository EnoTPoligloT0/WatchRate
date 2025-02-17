using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence.Movie;

public interface IMovieCrewRepository
{
    Task<IEnumerable<MovieCrew>> GetCrewByMovieId(MovieId movieId);
    Task<IEnumerable<MovieCrew>> GetById(MovieCrewId crewId);
    Task AddCrew(MovieId movieId, MovieCrew crew);
    Task UpdateCrew(MovieId movieId, MovieCrew crew);
    Task DeleteCrew(MovieCrew crew);
    Task DeleteCrewByMovieId(MovieId movieId, MovieCrewId crewId);
}
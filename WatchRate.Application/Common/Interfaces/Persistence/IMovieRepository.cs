using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using MovieAggregate = WatchRate.Domain.MovieAggregate.Movie;
namespace WatchRate.Application.Common.Interfaces.Persistence;

public interface IMovieRepository
{
    Task<MovieAggregate?> GetById(MovieId id);
    Task<MovieAggregate?> GetByDbId(string dbId);
    Task<IEnumerable<MovieAggregate>> GetAll(int page = 1, int pageSize = 10);
    Task<MovieAggregate> Create(MovieAggregate movie);
    void Update(MovieAggregate movie);
    void Delete(MovieId id);
    
    Task<IEnumerable<MovieAggregate>> Search(string searchTerm, int page = 1, int pageSize = 10);
    Task<IEnumerable<MovieAggregate>> GetByYear(short year, int page = 1, int pageSize = 10);
    Task<IEnumerable<MovieAggregate>> GetByGenre(string genre, int page = 1, int pageSize = 10);
    Task<IEnumerable<MovieAggregate>> GetByMaturityRating(MaturityRating rating, int page = 1, int pageSize = 10);
    
    Task UpdateRating(MovieId movieId, decimal newRating, int totalRatings);
    Task<decimal> GetAverageRating(MovieId movieId);
}
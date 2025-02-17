using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence.Movie;

public interface IStreamingPlatformRepository
{
    Task<IEnumerable<StreamingPlatform>> GetByMovieId(MovieId movieId);
    Task<IEnumerable<StreamingPlatform>> GetById(StreamingPlatformId platformId);
    Task AddPlatform(MovieId movieId, StreamingPlatform platform);
    Task UpdatePlatform(StreamingPlatform platform);
    Task DeletePlatform(StreamingPlatform platform);
    Task DeletePlatformByMovieId(MovieId movieId, StreamingPlatformId platformId);
}
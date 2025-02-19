using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence;

public interface IUserWatchlistRepository
{
    Task<IEnumerable<UserWatchlist>> GetByUserId(UserId userId);
    Task AddToWatchlist(UserId userId, MovieId movieId);
    Task DeleteFromWatchlist(UserId userId, MovieId movieId);
} 
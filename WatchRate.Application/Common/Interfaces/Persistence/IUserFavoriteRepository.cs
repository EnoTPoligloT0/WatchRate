using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence;

public interface IUserFavoriteRepository
{
    Task<IEnumerable<UserFavorite?>> GetByUserId(UserId userId);
    Task AddToFavorites(UserId userId, MovieId movieId);
    Task DeleteFromFavorites(UserId userId, MovieId movieId);
} 
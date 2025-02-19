using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;
using WatchRate.Domain.MovieAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence;

public interface IUserRatingRepository
{
    Task<IEnumerable<UserRating>> GetByUserId(UserId userId);
    Task AddRating(UserId userId, MovieId movieId, int value, string? review);
    Task UpdateRating(UserId userId, MovieId movieId, int value, string? review);
    Task DeleteRating(UserId userId, MovieId movieId);
} 
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate.Entities;

public class UserWatchlist : Entity<UserWatchlistId>
{
    public MovieId MovieId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }

    private UserWatchlist(UserWatchlistId userWatchlistId, MovieId movieId, DateTime createdDateTime)
    : base(userWatchlistId)
    {
        MovieId = movieId;
        CreatedDateTime = createdDateTime;
    }

    public static UserWatchlist Create(MovieId movieId, DateTime createdDateTime)
    {
        return new UserWatchlist(UserWatchlistId.CreateUnique(), movieId, DateTime.Now);
    }
}
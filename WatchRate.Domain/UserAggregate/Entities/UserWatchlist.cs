using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate.Entities;

public class UserWatchlist : Entity<UserWatchlistId>
{
    public MovieId MovieId { get; private set; }
    public DateTime AddedDateTime { get; private set; }

    private UserWatchlist(UserWatchlistId userWatchlistId, MovieId movieId, DateTime addedDateTime)
    : base(userWatchlistId)
    {
        MovieId = movieId;
        AddedDateTime = addedDateTime;
    }

    public static UserWatchlist Create(MovieId movieId, DateTime addedDateTime)
    {
        return new UserWatchlist(UserWatchlistId.CreateUnique(), movieId, addedDateTime);
    }
}
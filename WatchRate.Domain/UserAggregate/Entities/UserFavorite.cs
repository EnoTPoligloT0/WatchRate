using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate.Entities;

public class UserFavorite : Entity<UserFavoriteId>
{
    public MovieId MovieId { get; private set; }
    
    public DateTime AddedDateTime { get; private set; }

    private UserFavorite(UserFavoriteId userFavoriteId , MovieId movieId, DateTime addedDateTime)
    : base(userFavoriteId)
    {
        MovieId = movieId;
        AddedDateTime = addedDateTime;
    }

    public static UserFavorite Create(MovieId movieId, DateTime addedDateTime)
    {
        return new UserFavorite(UserFavoriteId.CreateUnique(), movieId, addedDateTime);
    }
}
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate.Entities;

public class UserRating : Entity<UserRatingId>
{
    public MovieId MovieId { get; private set; }
    public int? Value { get; private set; }
    public string? Review { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private UserRating(UserRatingId userRatingId, MovieId movieId, int? value, string? review, DateTime createdDateTime, DateTime? updatedDateTime)
    : base(userRatingId)
    {
        MovieId = movieId;
        Value = value;
        Review = review;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static UserRating Create(MovieId movieId, int? value, string? review, DateTime createdDateTime, DateTime? updatedDateTime)
    {
        return new UserRating(UserRatingId.CreateUnique(), movieId, value, review, DateTime.Now, DateTime.Now);
    }
}
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    private readonly List<UserWatchlistId> _userWatchlistIds = new();
    private readonly List<UserFavoriteId> _userFavoriteIds = new();
    private readonly List<UserRatingId> _userRatingIds = new();

    public string Email { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime CreatedDate { get; set; }

    public IReadOnlyList<UserWatchlistId> UserWatchlistIds => _userWatchlistIds.AsReadOnly();
    public IReadOnlyList<UserFavoriteId> UserFavoriteIds => _userFavoriteIds.AsReadOnly();
    public IReadOnlyList<UserRatingId> UserRatingIds => _userRatingIds.AsReadOnly();

    private User(UserId userId,
        string email,
        string userName,
        string passwordHash,
        string profilePictureUrl,
        DateTime createdDateTime,
        DateTime createdDate)
        : base(userId)
    {
        Email = email;
        UserName = userName;
        PasswordHash = passwordHash;
        ProfilePictureUrl = profilePictureUrl;
        CreatedDateTime = createdDateTime;
        CreatedDate = createdDate;
    }

    public static User Create(string email, string userName, string passwordHash, string profilePictureUrl)
    {
        return new User(UserId.CreateUnique(), email, userName, passwordHash, profilePictureUrl, DateTime.Now,
            DateTime.Now);
    }
}
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    private readonly List<UserWatchlist> _userWatchlists = new();
    private readonly List<UserFavorite> _userFavorites = new();
    private readonly List<UserRating> _userRatings = new();

    public string Email { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime CreatedDate { get; set; }

    public IReadOnlyList<UserWatchlist> UserWatchlists => _userWatchlists.AsReadOnly();
    public IReadOnlyList<UserFavorite> UserFavorites => _userFavorites.AsReadOnly();
    public IReadOnlyList<UserRating> UserRatings => _userRatings.AsReadOnly();

    private User() { }
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
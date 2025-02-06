using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.UserAggregate.ValueObjects;

public class UserRatingId : ValueObject, IEntityId<UserRatingId, Guid>
{
    public Guid Value { get; }
    public UserRatingId(Guid value)
    {
        Value = value;
    }
    
    public static UserRatingId CreateUnique() => new (Guid.NewGuid());

    public static UserRatingId Create(Guid value) => new (value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
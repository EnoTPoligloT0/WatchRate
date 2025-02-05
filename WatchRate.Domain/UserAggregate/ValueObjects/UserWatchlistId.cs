namespace WatchRate.Domain.UserAggregate.ValueObjects;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.StreamingAggregate.ValueObjects;


[EfCoreValueConverter(typeof(UserWatchlistIdValueConverter))]
public class UserWatchlistId : ValueObject, IEntityId<UserWatchlistId, Guid>
{
    public Guid Value { get; }
    
    public UserWatchlistId(Guid value)
    {
        Value = value;
    }
    
    public static UserWatchlistId CreateUnique() => new(Guid.NewGuid());

    public static UserWatchlistId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public class UserWatchlistIdValueConverter : ValueConverter<UserWatchlistId, Guid>
    {
        public UserWatchlistIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
    
}
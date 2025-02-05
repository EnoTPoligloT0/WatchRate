using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.UserAggregate.Entities;

namespace WatchRate.Domain.UserAggregate.ValueObjects;

[EfCoreValueConverter(typeof(UserFavoriteIdValueConverter))]
public class UserFavoriteId : ValueObject, IEntityId<UserFavoriteId, Guid>
{
    public Guid Value { get; }
    
    public UserFavoriteId(Guid value)
    {
        Value = value;
    }
    
    public static UserFavoriteId CreateUnique() => new(Guid.NewGuid());

    public static UserFavoriteId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public class UserFavoriteIdValueConverter : ValueConverter<UserFavoriteId, Guid>
    {
        public UserFavoriteIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
    
}
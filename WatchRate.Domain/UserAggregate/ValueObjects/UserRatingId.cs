using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.UserAggregate.Entities;

namespace WatchRate.Domain.UserAggregate.ValueObjects;

[EfCoreValueConverter(typeof(UserRatingIdValueConverter))]
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
    
    public class 
    UserRatingIdValueConverter : ValueConverter<UserRatingId, Guid>
    {
        public UserRatingIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
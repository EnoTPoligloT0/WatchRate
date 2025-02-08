using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Domain.UserAggregate.ValueObjects;

[EfCoreValueConverter(typeof(UserIdValueConverter))]
public class UserId : ValueObject, IEntityId<UserId, Guid>
{
    public Guid Value { get; }
    
    public UserId(Guid value)
    {
        Value = value;
    }
    
    public static UserId CreateUnique() => new(Guid.NewGuid());

    public static UserId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    public class UserIdValueConverter : ValueConverter<UserId, Guid>
    {
        public UserIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
    
}
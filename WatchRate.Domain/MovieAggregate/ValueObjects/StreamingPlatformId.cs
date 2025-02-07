using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.StreamingAggregate.ValueObjects;

[EfCoreValueConverter(typeof(StreamingPlatformIdValueConverter))]
public class StreamingPlatformId : ValueObject, IEntityId<StreamingPlatformId, Guid>
{
    public Guid Value { get; }

    private StreamingPlatformId(Guid value)
    {
        Value = value;
    }

    public static StreamingPlatformId CreateUnique() => new(Guid.NewGuid());
    public static StreamingPlatformId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class StreamingPlatformIdValueConverter : ValueConverter<StreamingPlatformId, Guid>
    {
        public StreamingPlatformIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
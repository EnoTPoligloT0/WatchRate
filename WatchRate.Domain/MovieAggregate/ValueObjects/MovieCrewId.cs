using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.MovieAggregate.ValueObjects;

[EfCoreValueConverter(typeof(MovieCrewIdValueConverter))]
public class MovieCrewId : ValueObject, IEntityId<MovieCrewId, Guid>
{
    public Guid Value { get; }

    private MovieCrewId(Guid value)
    {
        Value = value;
    }

    public static MovieCrewId CreateUnique() => new(Guid.NewGuid());
    public static MovieCrewId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class MovieCrewIdValueConverter : ValueConverter<MovieCrewId, Guid>
    {
        public MovieCrewIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
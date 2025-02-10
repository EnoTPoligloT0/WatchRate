using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.MovieAggregate.ValueObjects;

[EfCoreValueConverter(typeof(MovieIdValueConverter))]
public sealed class MovieId : ValueObject, IEntityId<MovieId, Guid>
{
    public Guid Value { get; }

    private MovieId(Guid value)
    {
        Value = value;
    }
    
    public static MovieId CreateUnique() => new MovieId(Guid.NewGuid());
    public static MovieId Create(Guid value) => new(value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class MovieIdValueConverter : ValueConverter<MovieId, Guid>
    {
        public MovieIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
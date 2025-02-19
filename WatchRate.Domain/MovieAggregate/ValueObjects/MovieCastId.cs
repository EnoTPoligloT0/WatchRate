using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.MovieAggregate;

[EfCoreValueConverter(typeof(MovieCastIdValueConverter))]
public class MovieCastId : ValueObject, IEntityId<MovieCastId, Guid> 
{
    public Guid Value { get; }
    public MovieCastId(Guid value)
    {
        Value = value;
    }
    
    public static MovieCastId CreateUnique() => new(Guid.NewGuid());

    public static MovieCastId Create(Guid value) => new(value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public class MovieCastIdValueConverter : ValueConverter<MovieCastId, Guid>
    {
        public MovieCastIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
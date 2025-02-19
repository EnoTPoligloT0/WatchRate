using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.MovieAggregate.ValueObjects;

[EfCoreValueConverter(typeof(MovieGenreIdValueConverter))]
public class MovieGenreId : ValueObject, IEntityId<MovieGenreId, Guid> 
{
    public Guid Value { get; }

    private MovieGenreId(Guid value)
    {
        Value = value;
    }
            
    public static MovieGenreId CreateUnique() => new(Guid.NewGuid());
    public static MovieGenreId Create(Guid value) => new(value);
            
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class MovieGenreIdValueConverter : ValueConverter<MovieGenreId, Guid>
    {
        public MovieGenreIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.MovieAggregate;

[EfCoreValueConverter(typeof(MovieCastId))]
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

}
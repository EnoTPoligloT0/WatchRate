using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.Common.Models;

namespace WatchRate.Domain.PersonAggregate.ValueObjects;

[EfCoreValueConverter(typeof(PersonIdValueConverter))]
public class PersonId : ValueObject, IEntityId<PersonId, Guid>
{
    public Guid Value { get; }

    public PersonId(Guid value)
    {
        Value = value;
    }
    
    public static PersonId CreateUnique() => new PersonId(Guid.NewGuid());

    public static PersonId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public class PersonIdValueConverter : ValueConverter<PersonId, Guid>
    {
        public  PersonIdValueConverter () : 
            base(
                id => id.Value,
                value => Create(value)
                ){}
    }
    
}
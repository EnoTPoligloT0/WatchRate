namespace WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.PersonAggregate;
using WatchRate.Domain.PersonAggregate.ValueObjects;

public interface IPersonRepository
{
    Task<Person?> GetById(PersonId id);
    IEnumerable<Person> GetAll(int page , int pageSize );
    Task<Person?> Create(Person person);
    void Update(Person person);
    void Delete(PersonId id);
    
    IEnumerable<Person> Search(string searchTerm, int pageSize);
    IEnumerable<Person> GetByName(string name, int page , int pageSize);
    IEnumerable<Person> GetByBirthPlace(string birthPlace, int page, int pageSize );
}
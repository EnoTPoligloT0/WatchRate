using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.PersonAggregate;
using WatchRate.Domain.PersonAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly WatchRateDbContext _context;

    public PersonRepository(WatchRateDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetById(PersonId id)
    {
        return await _context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public IEnumerable<Person> GetAll(int page, int pageSize)
    {
        return _context.Persons
            .AsNoTracking()
            .OrderBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public async Task<Person?> Create(Person person)
    {
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public void Update(Person person)
    {
        _context.Persons.Update(person);
        _context.SaveChanges();
    }

    public void Delete(PersonId id)
    {
        var person = _context.Persons.Find(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Person> Search(string searchTerm, int pageSize)
    {
        return _context.Persons
            .AsNoTracking()
            .Where(p => p.FirstName.Contains(searchTerm) || 
                       p.LastName.Contains(searchTerm) || 
                       p.Biography!.Contains(searchTerm))
            .Take(pageSize)
            .ToList();
    }

    public IEnumerable<Person> GetByName(string name, int page, int pageSize)
    {
        return _context.Persons
            .AsNoTracking()
            .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name))
            .OrderBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public IEnumerable<Person> GetByBirthPlace(string birthPlace, int page, int pageSize)
    {
        return _context.Persons
            .AsNoTracking()
            .Where(p => p.BirthPlace!.Contains(birthPlace))
            .OrderBy(p => p.LastName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
} 
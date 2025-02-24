using FluentAssertions;
using WatchRate.Domain.PersonAggregate;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Infrastucture.Persistence.Repositories;
using WatchRate.Tests.Common;
using Xunit;

namespace WatchRate.Tests.RepostioryTests;

public class PersonRepositoryTests : TestBase
{
    private readonly PersonRepository _sut;

    public PersonRepositoryTests()
    {
        _sut = new PersonRepository(Context);
    }

    [Fact]
    public async Task GetById_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var person = Person.Create(
            firstName: "John",
            lastName: "Doe",
            biography: "Test biography",
            birthDate: "1990-01-01",
            birthPlace: "New York",
            profileImageUrl: "http://example.com/image.jpg",
            createdDateTime: DateTime.UtcNow,
            updatedDateTime: DateTime.UtcNow);

        await Context.Persons.AddAsync(person);
        await Context.SaveChangesAsync();

        // Act
        var result = await _sut.GetById(person.Id);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("John");
        result.LastName.Should().Be("Doe");
    }

    [Fact]
    public void GetAll_ShouldReturnPaginatedResults()
    {
        // Arrange
        var persons = new List<Person>
        {
            CreateTestPerson("John", "Doe"),
            CreateTestPerson("Jane", "Smith"),
            CreateTestPerson("Bob", "Johnson")
        };
        Context.Persons.AddRange(persons);
        Context.SaveChanges();

        // Act
        var result = _sut.GetAll(page: 1, pageSize: 2).ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task Create_ShouldAddNewPerson()
    {
        // Arrange
        var person = CreateTestPerson("John", "Doe");

        // Act
        var result = await _sut.Create(person);

        // Assert
        result.Should().NotBeNull();
        Context.Persons.Should().Contain(p => p.Id == person.Id);
    }

    [Fact]
    public void Search_ShouldReturnMatchingResults()
    {
        // Arrange
        var persons = new List<Person>
        {
            CreateTestPerson("John", "Doe", "Test bio"),
            CreateTestPerson("Jane", "Smith", "Another bio"),
            CreateTestPerson("John", "Smith", "Different bio")
        };
        Context.Persons.AddRange(persons);
        Context.SaveChanges();

        // Act
        var result = _sut.Search("John", 10).ToList();

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(p => p.FirstName.Should().Be("John"));
    }

    [Fact]
    public void GetByBirthPlace_ShouldReturnMatchingResults()
    {
        // Arrange
        var persons = new List<Person>
        {
            CreateTestPerson("John", "Doe", birthPlace: "New York"),
            CreateTestPerson("Jane", "Smith", birthPlace: "Los Angeles"),
            CreateTestPerson("Bob", "Johnson", birthPlace: "New York")
        };
        Context.Persons.AddRange(persons);
        Context.SaveChanges();

        // Act
        var result = _sut.GetByBirthPlace("New York", 1, 10).ToList();

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(p => p.BirthPlace.Should().Be("New York"));
    }

    private static Person CreateTestPerson(
        string firstName, 
        string lastName, 
        string? biography = null, 
        string? birthPlace = null)
    {
        return Person.Create(
            firstName,
            lastName,
            biography,
            "1990-01-01",
            birthPlace,
            "http://example.com/image.jpg",
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
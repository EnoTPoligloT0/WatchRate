using WatchRate.Domain.Common.Models;
using WatchRate.Domain.PersonAggregate.ValueObjects;

namespace WatchRate.Domain.PersonAggregate;

public class Person : AggregateRoot<PersonId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Biography { get; set; }
    public string? BirthDate { get; set; }
    public string? BirthPlace { get; set; }
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    private Person(
        PersonId id,
        string firstName,
        string lastName,
        string? biography,
        string? birthDate,
        string? birthPlace,
        string? profileImageUrl,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        FirstName = firstName;
        LastName = lastName;
        Biography = biography;
        BirthDate = birthDate;
        BirthPlace = birthPlace;
        ProfileImageUrl = profileImageUrl;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Person Create(string firstName, string lastName, string? biography, string? birthDate, string? birthPlace, string? profileImageUrl, DateTime createdDateTime, DateTime updatedDateTime)
    {
        return new Person(PersonId.CreateUnique(), firstName, lastName, biography, birthDate, birthPlace, profileImageUrl, DateTime.Now, DateTime.Now);
    }
}
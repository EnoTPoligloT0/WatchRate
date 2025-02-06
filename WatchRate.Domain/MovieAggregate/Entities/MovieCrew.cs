using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.PersonAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate;

public class MovieCrew : Entity<MovieCrewId>
{
    public PersonId PersonId { get; set; }
    public string Role { get; set; }
    public string Department { get; set; }

    private MovieCrew(MovieCrewId movieCrewId, PersonId personId, string role, string department)
        : base(movieCrewId)
    {
        PersonId = personId;
        Role = role;
        Department = department;
    }

    public static MovieCrew Create(PersonId personId, string role, string department)
    {
        return new MovieCrew(MovieCrewId.CreateUnique() , personId, role, department);
    }
}
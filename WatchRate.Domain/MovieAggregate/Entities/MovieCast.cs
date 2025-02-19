using WatchRate.Domain.Common.Models;
using WatchRate.Domain.PersonAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate.Entities;

public class MovieCast : Entity<MovieCastId>
{
    public PersonId PersonId { get; set; }
    public string? Character { get; set; }
    public string? Order { get; set; }

    private MovieCast() { }
    private MovieCast(MovieCastId movieCastId, PersonId personId, string? character, string? order)
        : base(movieCastId)
    {
        PersonId = personId;
        Character = character;
        Order = order;
    }

    public static MovieCast Create(PersonId personId, string character, string order)
    {
        return new MovieCast(MovieCastId.CreateUnique(), personId, character, order);
    }
}
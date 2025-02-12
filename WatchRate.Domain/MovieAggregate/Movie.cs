using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate;

public class Movie : AggregateRoot<MovieId>
{
    private readonly List<MovieGenre> _genres = new();
    private readonly List<MovieCast> _cast = new();
    private readonly List<MovieCrew> _crew = new();
    private readonly List<StreamingPlatform> _streamingPlatfoms = new();

    public string? DbId { get; protected set; }
    public string Title { get; protected set; } = null!;
    public string Description { get; protected set; } = null!;
    public short Year { get; protected set; }
    public short? Runtime { get; protected set; }
    public MaturityRating? MaturityRating { get; protected set; }
    public decimal AverageRating { get; protected set; }
    public int? TotalRatings { get; private set; }
    public string? PosterUrl { get; private set; }
    public string? BackdropUrl { get; private set; }
    public string? TrailerUrl { get; private set; }
    public DateTime CreatedDateTime { get; protected set; }
    public DateTime? UpdatedDateTime { get; protected set; }

    public IReadOnlyList<MovieGenre> MovieGenres => _genres.AsReadOnly();
    public IReadOnlyList<MovieCast> MovieCasts => _cast.AsReadOnly();
    public IReadOnlyList<MovieCrew> MovieCrews => _crew.AsReadOnly();
    public IReadOnlyList<StreamingPlatform> StreamingPlatforms => _streamingPlatfoms.AsReadOnly();

    public Movie(
        MovieId movieId,
        string title,
        string description,
        short year,
        short? runtime,
        MaturityRating maturityRating,
        DateTime createdDatetime,
        DateTime updatedDatetime) : base(movieId)
    {
        Title = title;
        Description = description;
        Year = year;
        Runtime = runtime;
        MaturityRating = maturityRating;
        CreatedDateTime = createdDatetime;
        UpdatedDateTime = updatedDatetime;
    }

    public static Movie Create(
        string title,
        string description,
        short year,
        short? runtime,
        MaturityRating maturityRating)
    {
        return new Movie(MovieId.CreateUnique(), title, description, year, runtime, maturityRating, DateTime.Now,
            DateTime.Now);
    }

    public void AddCastMember(MovieCast cast) => _cast.Add(cast);

    public void RemoveCastMember(MovieCast cast) => _cast.Remove(cast);

    public void AddCrewMember(MovieCrew crewId) => _crew.Add(crewId);

    public void RemoveCrewMember(MovieCrew crew) => _crew.Remove(crew);

    public void AddGenre(MovieGenre genre) => _genres.Add(genre);

    public void RemoveGenre(MovieGenre genre) => _genres.Remove(genre);

    public void AddStreamingPlatform(StreamingPlatform platform) => _streamingPlatfoms.Add(platform);

    public void RemoveStreamingPlatform(StreamingPlatform platform) => _streamingPlatfoms.Remove(platform);

    public void UpdateRating(decimal newRating, int totalRatings)
    {
        AverageRating = newRating;
        TotalRatings = totalRatings;
    }
}
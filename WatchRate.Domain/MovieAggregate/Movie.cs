using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate;

public class Movie : AggregateRoot<MovieId>
{
    private readonly List<MovieGenreId> _genreIds = new();
    private readonly List<MovieCastId> _castIds = new();
    private readonly List<MovieCrewId> _crewIds = new();
    private readonly List<StreamingPlatformId> _streamingPlatfomIds = new();

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

    public IReadOnlyList<MovieGenreId> MovieGenreIds => _genreIds.AsReadOnly();
    public IReadOnlyList<MovieCastId> MovieCastIds => _castIds.AsReadOnly();
    public IReadOnlyList<MovieCrewId> MovieCrewIds => _crewIds.AsReadOnly();
    public IReadOnlyList<StreamingPlatformId> StreamingPlatformIds => _streamingPlatfomIds.AsReadOnly();

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

    public void AddCastMember(MovieCastId castId) => _castIds.Add(castId);

    public void RemoveCastMember(MovieCastId castId) => _castIds.Remove(castId);

    public void AddCrewMember(MovieCrewId crewId) => _crewIds.Add(crewId);

    public void RemoveCrewMember(MovieCrewId crewId) => _crewIds.Remove(crewId);

    public void AddGenre(MovieGenreId genreId) => _genreIds.Add(genreId);

    public void RemoveGenre(MovieGenreId genreId) => _genreIds.Remove(genreId);

    public void AddStreamingPlatform(StreamingPlatformId platformId) => _streamingPlatfomIds.Add(platformId);

    public void RemoveStreamingPlatform(StreamingPlatformId platformId) => _streamingPlatfomIds.Remove(platformId);

    public void UpdateRating(decimal newRating, int totalRatings)
    {
        AverageRating = newRating;
        TotalRatings = totalRatings;
    }
}
using WatchRate.Domain.Common.Models;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Domain.MovieAggregate;

public class Movie : AggregateRoot<MovieId>
{
    private readonly List<MovieGenre> _movieGenres = new();
    private readonly List<MovieCast> _movieCasts = new();
    private readonly List<MovieCrew> _movieCrews = new();
    private readonly List<StreamingPlatform> _streamingPlatforms = new();

    public string? DbId { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public short Year { get; private set; }
    public short? Runtime { get; private set; }
    public MaturityRating? MaturityRating { get; private set; }
    public decimal AverageRating { get; private set; }
    public int? TotalRatings { get; private set; }
    public string? PosterUrl { get; private set; }
    public string? BackdropUrl { get; private set; }
    public string? TrailerUrl { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    
    public IReadOnlyList<MovieGenre> MovieGenres => _movieGenres.AsReadOnly();
    public IReadOnlyList<MovieCast> MovieCasts => _movieCasts.AsReadOnly();
    public IReadOnlyList<MovieCrew> MovieCrews => _movieCrews.AsReadOnly();
    public IReadOnlyList<StreamingPlatform> StreamingPlatforms => _streamingPlatforms.AsReadOnly();

    private Movie(
        MovieId movieId,
        string title,
        string description,
        short year,
        short? runtime,
        MaturityRating? maturityRating,
        DateTime createdDateTime) : base(movieId)
    {
        Title = title;
        Description = description;
        Year = year;
        Runtime = runtime;
        MaturityRating = maturityRating;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = createdDateTime;
    }

    public static Movie Create(
        string title,
        string description,
        short year,
        short? runtime,
        MaturityRating? maturityRating)
    {
        return new Movie(
            MovieId.CreateUnique(), 
            title, 
            description, 
            year, 
            runtime, 
            maturityRating,
            DateTime.UtcNow);
    }
    
    public void AddCastMember(MovieCast cast) => _movieCasts.Add(cast);
    public void RemoveCastMember(MovieCast cast) => _movieCasts.Remove(cast);
    public void AddCrewMember(MovieCrew crew) => _movieCrews.Add(crew);
    public void RemoveCrewMember(MovieCrew crew) => _movieCrews.Remove(crew);
    public void AddGenre(MovieGenre genre) => _movieGenres.Add(genre);
    public void RemoveGenre(MovieGenre genre) => _movieGenres.Remove(genre);
    public void AddStreamingPlatform(StreamingPlatform platform) => _streamingPlatforms.Add(platform);
    public void RemoveStreamingPlatform(StreamingPlatform platform) => _streamingPlatforms.Remove(platform);
    public void UpdateRating(decimal newRating, int totalRatings)
    {
        AverageRating = newRating;
        TotalRatings = totalRatings;
    }
    
#pragma warning disable CS8618
    private Movie() 
#pragma warning restore CS8618
    {
    }
}    


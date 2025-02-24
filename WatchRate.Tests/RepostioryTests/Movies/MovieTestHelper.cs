using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;

public static class MovieTestHelper
{
    public static Movie CreateTestMovie(
        string title = "Test Movie",
        string description = "Test Description",
        short year = 2024)
    {
        return Movie.Create(
            title,
            description,
            year,
            100,
            MaturityRating.G);
    }

    public static MovieCast CreateTestMovieCast(
        PersonId personId,
        string character = "Test Character",
        string order = "1")
    {
        return MovieCast.Create(personId, character, order);
    }

    public static MovieCrew CreateTestMovieCrew(
        PersonId personId,
        string role = "Director",
        string department = "Directing")
    {
        return MovieCrew.Create(personId, role, department);
    }

    public static MovieGenre CreateTestMovieGenre(string name = "Action")
    {
        return MovieGenre.Create(name);
    }

    public static StreamingPlatform CreateTestStreamingPlatform(
        string name = "Netflix",
        string url = "http://netflix.com/movie")
    {
        return StreamingPlatform.Create(name, url);
    }
} 
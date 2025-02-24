using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WatchRate.Infrastucture.Persistence;
using WatchRate.Infrastucture.Persistence.Repositories;

namespace WatchRate.Tests.Common;

public class StreamingPlatformRepositoryTests : TestBase
{
    private readonly StreamingPlatformRepository _sut;

    public StreamingPlatformRepositoryTests()
    {
        _sut = new StreamingPlatformRepository(Context);
    }

    [Fact]
    public async Task GetByMovieId_ShouldReturnPlatforms_WhenMovieExists()
    {
        // Arrange
        var movie = MovieTestHelper.CreateTestMovie();
        var platform = MovieTestHelper.CreateTestStreamingPlatform();
        movie.AddStreamingPlatform(platform);
        
        await Context.Movies.AddAsync(movie);
        await Context.SaveChangesAsync();

        // Act
        var result = await _sut.GetByMovieId(movie.Id);

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Netflix");
    }

    [Fact]
    public async Task AddPlatform_ShouldAddNewPlatform()
    {
        // Arrange
        var movie = MovieTestHelper.CreateTestMovie();
        await Context.Movies.AddAsync(movie);
        await Context.SaveChangesAsync();

        var platform = MovieTestHelper.CreateTestStreamingPlatform();

        // Act
        await _sut.AddPlatform(movie.Id, platform);

        // Assert
        var result = await Context.Movies
            .Include(m => m.StreamingPlatforms)
            .FirstAsync(m => m.Id == movie.Id);
        result.StreamingPlatforms.Should().HaveCount(1);
    }
} 
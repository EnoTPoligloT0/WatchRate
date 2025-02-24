using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using WatchRate.Domain.MovieAggregate;
using Xunit;
using WatchRate.Infrastucture.Persistence;
using WatchRate.Infrastucture.Persistence.Repositories;
using WatchRate.Tests.Common;

namespace WatchRate.Tests.RepostioryTests.Movies
{
    public class MovieRepositoryTests : TestBase
    {
        private readonly MovieRepository _sut;

        public MovieRepositoryTests()
        {
            _sut = new MovieRepository(Context);
        }

        [Fact]
        public async Task GetById_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(movie.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("Test Movie");
        }

        [Fact]
        public async Task GetByDbId_ShouldReturnMovie_WhenMovieExists()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetByDbId(movie.DbId);

            // Assert
            result.Should().NotBeNull();
            result!.Title.Should().Be("Test Movie");
        }

        [Fact]
        public async Task GetAll_ShouldReturnPaginatedResults()
        {
            // Arrange
            var movies = new List<Movie>
            {
                MovieTestHelper.CreateTestMovie("Movie 1"),
                MovieTestHelper.CreateTestMovie("Movie 2"),
                MovieTestHelper.CreateTestMovie("Movie 3")
            };
            await Context.Movies.AddRangeAsync(movies);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetAll(page: 1, pageSize: 2);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Search_ShouldReturnMatchingResults()
        {
            // Arrange
            var movies = new List<Movie>
            {
                MovieTestHelper.CreateTestMovie("Star Wars"),
                MovieTestHelper.CreateTestMovie("Star Trek"),
                MovieTestHelper.CreateTestMovie("Avengers")
            };
            await Context.Movies.AddRangeAsync(movies);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.Search("Star");

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(m => m.Title.Should().Contain("Star"));
        }

        [Fact]
        public async Task Create_ShouldAddNewMovie()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();

            // Act
            var result = await _sut.Create(movie);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Test Movie");
            Context.Movies.Should().Contain(m => m.Id == movie.Id);
        }
        
        [Fact]
        public async Task Update_ShouldUpdateMovieAndRelatedEntities()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Create a new movie with the same ID but updated properties
            var updatedMovie = MovieTestHelper.CreateTestMovie(
                title: "Updated Title",
                description: "Updated Description");
            
            // Update the existing movie's properties
            movie.UpdateDetails(
                title: "Updated Title",
                description: "Updated Description",
                year: movie.Year);

            // Act
            _sut.Update(movie);

            // Assert
            var result = await Context.Movies.FindAsync(movie.Id);
            result.Should().NotBeNull();
            result!.Title.Should().Be("Updated Title");
            result.Description.Should().Be("Updated Description");
        }
        
        [Fact]
        public async Task Delete_ShouldRemoveMovie()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
             _sut.Delete(movie.Id);

            // Assert
            var deletedMovie = await Context.Movies.FindAsync(movie.Id);
            deletedMovie.Should().BeNull();
        }

        [Fact]
        public async Task Search_ShouldReturnMatchingMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                MovieTestHelper.CreateTestMovie("Star Wars"),
                MovieTestHelper.CreateTestMovie("Star Trek"),
                MovieTestHelper.CreateTestMovie("Avengers")
            };
            await Context.Movies.AddRangeAsync(movies);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.Search("Star");

            // Assert
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(m => m.Title.Should().Contain("Star"));
        }
    }
} 
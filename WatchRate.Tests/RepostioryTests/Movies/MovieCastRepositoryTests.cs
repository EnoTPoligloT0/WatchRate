using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WatchRate.Infrastucture.Persistence;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Infrastucture.Persistence.Repositories;
using WatchRate.Tests.Common;

namespace WatchRate.Tests.RepostioryTests.Movies
{
    public class MovieCastRepositoryTests : TestBase
    {
        private readonly MovieCastRepository _sut;

        public MovieCastRepositoryTests()
        {
            _sut = new MovieCastRepository(Context);
        }

        [Fact]
        public async Task GetByMovieId_ShouldReturnCasts_WhenMovieExists()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            var personId = PersonId.CreateUnique();
            var cast = MovieTestHelper.CreateTestMovieCast(personId);
            movie.AddCastMember(cast);
            
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetByMovieId(movie.Id);

            // Assert
            result.Should().HaveCount(1);
            result.First().Character.Should().Be("Test Character");
        }

        [Fact]
        public async Task AddCast_ShouldAddNewCastMember()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            var cast = MovieTestHelper.CreateTestMovieCast(PersonId.CreateUnique());

            // Act
            await _sut.AddCast(movie.Id, cast);

            // Assert
            var result = await Context.Movies
                .Include(m => m.MovieCasts)
                .FirstAsync(m => m.Id == movie.Id);
            result.MovieCasts.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdateCast_ShouldModifyExistingCastMember()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            var personId = PersonId.CreateUnique();
            var cast = MovieTestHelper.CreateTestMovieCast(personId);
            movie.AddCastMember(cast);
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            cast.Character = "Updated Character";

            // Act
            await _sut.UpdateCast(movie.Id, cast);

            // Assert
            var result = await Context.Movies
                .Include(m => m.MovieCasts)
                .FirstAsync(m => m.Id == movie.Id);
            result.MovieCasts.First().Character.Should().Be("Updated Character");
        }

        [Fact]
        public async Task DeleteCast_ShouldRemoveCastMember()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            var personId = PersonId.CreateUnique();
            var cast = MovieTestHelper.CreateTestMovieCast(personId);
            movie.AddCastMember(cast);
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            await _sut.DeleteCastByMovieId(movie.Id, cast.Id);

            // Assert
            var result = await Context.Movies
                .Include(m => m.MovieCasts)
                .FirstAsync(m => m.Id == movie.Id);
            result.MovieCasts.Should().BeEmpty();
        }
    }
} 
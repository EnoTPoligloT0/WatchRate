using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WatchRate.Infrastucture.Persistence;
using WatchRate.Tests.Common;
using WatchRate.Infrastucture.Persistence.Repositories;

namespace WatchRate.Tests.RepostioryTests.Movies
{
    public class MovieGenreRepositoryTests : TestBase
    {
        private readonly MovieGenreRepository _sut;

        public MovieGenreRepositoryTests()
        {
            _sut = new MovieGenreRepository(Context);
        }

        [Fact]
        public async Task GetByMovieId_ShouldReturnGenres_WhenMovieExists()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            var genre = MovieTestHelper.CreateTestMovieGenre();
            movie.AddGenre(genre);
            
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetByMovieId(movie.Id);

            // Assert
            result.Should().HaveCount(1);
            result.First().Name.Should().Be("Action");
        }

        [Fact]
        public async Task AddGenre_ShouldAddNewGenre()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            var genre = MovieTestHelper.CreateTestMovieGenre();

            // Act
            await _sut.AddGenre(movie.Id, genre);

            // Assert
            var result = await Context.Movies
                .Include(m => m.MovieGenres)
                .FirstAsync(m => m.Id == movie.Id);
            result.MovieGenres.Should().HaveCount(1);
        }
    }
} 
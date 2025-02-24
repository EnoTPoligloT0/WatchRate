using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Infrastucture.Persistence.Repositories;
using WatchRate.Tests.Common;

namespace WatchRate.Tests.Repositories.Movies
{
    public class MovieCrewRepositoryTests : TestBase
    {
        private readonly MovieCrewRepository _sut;

        public MovieCrewRepositoryTests()
        {
            _sut = new MovieCrewRepository(Context);
        }

        [Fact]
        public async Task GetByMovieId_ShouldReturnCrew_WhenMovieExists()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            var personId = PersonId.CreateUnique();
            var crew = MovieTestHelper.CreateTestMovieCrew(personId);
            movie.AddCrewMember(crew);
            
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            // Act
            var result = await _sut.GetCrewByMovieId(movie.Id);

            // Assert
            result.Should().HaveCount(1);
            result.First().Role.Should().Be("Director");
        }

        [Fact]
        public async Task AddCrew_ShouldAddNewCrewMember()
        {
            // Arrange
            var movie = MovieTestHelper.CreateTestMovie();
            await Context.Movies.AddAsync(movie);
            await Context.SaveChangesAsync();

            var crew = MovieTestHelper.CreateTestMovieCrew(PersonId.CreateUnique());

            // Act
            await _sut.AddCrew(movie.Id, crew);

            // Assert
            var result = await Context.Movies
                .Include(m => m.MovieCrews)
                .FirstAsync(m => m.Id == movie.Id);
            result.MovieCrews.Should().HaveCount(1);
        }
    }
} 
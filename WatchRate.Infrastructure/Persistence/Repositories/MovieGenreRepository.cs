using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence.Movie;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.MovieAggregate.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private readonly WatchRateDbContext _context;

        public MovieGenreRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieGenre>> GetByMovieId(MovieId movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.MovieGenres ?? Enumerable.Empty<MovieGenre>();
        }

        public async Task AddGenre(MovieId movieId, MovieGenre genre)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.AddGenre(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenre(MovieGenre genre)
        {
            var existingGenre = await _context.MovieGenres
                .FirstOrDefaultAsync(g => g.Id == genre.Id);

            if (existingGenre == null)
                throw new KeyNotFoundException("Movie genre not found");

            // Update properties (for example, the Name property).
            existingGenre.Name = genre.Name;
            // Update any additional properties as needed.

            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(MovieId movieId, MovieGenreId genreId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var genre = movie.MovieGenres.FirstOrDefault(g => g.Id == genreId);
            if (genre != null)
            {
                movie.RemoveGenre(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
} 
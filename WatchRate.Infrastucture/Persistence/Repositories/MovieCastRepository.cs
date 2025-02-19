using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using WatchRate.Application.Common.Interfaces.Persistence.Movie;
using WatchRate.Domain.MovieAggregate.Entities;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class MovieCastRepository : IMovieCastRepository
    {
        private readonly WatchRateDbContext _context;

        public MovieCastRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieCast>> GetByMovieId(MovieId movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCasts)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.MovieCasts ?? Enumerable.Empty<MovieCast>();
        }

        public async Task<IEnumerable<MovieCast>> GetById(MovieCastId castId)
        {
            return await _context.MovieCasts
                .Where(c => c.Id == castId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddCast(MovieId movieId, MovieCast cast)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCasts)
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.AddCastMember(cast);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCast(MovieId movieId, MovieCast updatedCast)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCasts)
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var existingCast = movie.MovieCasts.FirstOrDefault(c => c.Id == updatedCast.Id);
            if (existingCast == null)
                throw new KeyNotFoundException("Movie cast not found");

            // Update properties
            existingCast.Character = updatedCast.Character;
            existingCast.Order = updatedCast.Order;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCast(MovieCast cast)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCasts)
                .FirstOrDefaultAsync(m => m.MovieCasts.Any(c => c.Id == cast.Id));
            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.RemoveCastMember(cast);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCastByMovieId(MovieId movieId, MovieCastId castId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCasts)
                .FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var cast = movie.MovieCasts.FirstOrDefault(c => c.Id == castId);
            if (cast != null)
            {
                movie.RemoveCastMember(cast);
                await _context.SaveChangesAsync();
            }
        }
    }
}
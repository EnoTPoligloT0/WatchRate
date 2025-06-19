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
    public class MovieCrewRepository : IMovieCrewRepository
    {
        private readonly WatchRateDbContext _context;

        public MovieCrewRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieCrew>> GetCrewByMovieId(MovieId movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCrews)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.MovieCrews ?? Enumerable.Empty<MovieCrew>();
        }

        public async Task<IEnumerable<MovieCrew>> GetById(MovieCrewId crewId)
        {
            return await _context.MovieCrews
                .Where(c => c.Id == crewId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddCrew(MovieId movieId, MovieCrew crew)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCrews)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.AddCrewMember(crew);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCrew(MovieId movieId, MovieCrew crew)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCrews)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var existingCrew = movie.MovieCrews.FirstOrDefault(c => c.Id == crew.Id);
            if (existingCrew == null)
                throw new KeyNotFoundException("Movie crew not found");

            // Update properties as required (for example, Role and Department).
            existingCrew.Role = crew.Role;
            existingCrew.Department = crew.Department;
            // Update additional properties as needed.

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCrew(MovieCrew crew)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCrews)
                .FirstOrDefaultAsync(m => m.MovieCrews.Any(c => c.Id == crew.Id));

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.RemoveCrewMember(crew);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCrewByMovieId(MovieId movieId, MovieCrewId crewId)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieCrews)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var crew = movie.MovieCrews.FirstOrDefault(c => c.Id == crewId);
            if (crew != null)
            {
                movie.RemoveCrewMember(crew);
                await _context.SaveChangesAsync();
            }
        }
    }
} 
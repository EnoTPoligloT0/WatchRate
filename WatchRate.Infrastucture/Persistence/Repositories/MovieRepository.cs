using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly WatchRateDbContext _context;

        public MovieRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<Movie?> GetById(MovieId id)
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie?> GetByDbId(string dbId)
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DbId == dbId);
        }

        public async Task<IEnumerable<Movie>> GetAll(int page = 1, int pageSize = 10)
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        public void Delete(MovieId id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Movie>> Search(string searchTerm, int page = 1, int pageSize = 10)
        {
            return await _context.Movies
                .Where(m => m.Title.Contains(searchTerm) || m.Description.Contains(searchTerm))
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByYear(short year, int page = 1, int pageSize = 10)
        {
            return await _context.Movies
                .Where(m => m.Year == year)
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByGenre(string genre, int page = 1, int pageSize = 10)
        {
            return await _context.Movies
                .Where(m => m.MovieGenres.Any(g => g.Name == genre))
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByMaturityRating(MaturityRating rating, int page = 1, int pageSize = 10)
        {
            return await _context.Movies
                .Where(m => m.MaturityRating == rating)
                .Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts)
                .Include(m => m.MovieCrews)
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateRating(MovieId movieId, decimal newRating, int totalRatings)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie != null)
            {
                movie.UpdateRating(newRating, totalRatings);
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetAverageRating(MovieId movieId)
        {
            var movie = await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.AverageRating ?? 0;
        }
    }
} 
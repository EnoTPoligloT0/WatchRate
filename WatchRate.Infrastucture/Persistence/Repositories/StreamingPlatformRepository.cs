using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence.Movie;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.StreamingAggregate.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class StreamingPlatformRepository : IStreamingPlatformRepository
    {
        private readonly WatchRateDbContext _context;

        public StreamingPlatformRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StreamingPlatform>> GetByMovieId(MovieId movieId)
        {
            var movie = await _context.Movies
                .Include(m => m.StreamingPlatforms)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie?.StreamingPlatforms ?? Enumerable.Empty<StreamingPlatform>();
        }

        public async Task<IEnumerable<StreamingPlatform>> GetById(StreamingPlatformId platformId)
        {
            return await _context.StreamingPlatforms
                .AsNoTracking()
                .Where(p => p.Id == platformId)
                .ToListAsync();
        }

        public async Task AddPlatform(MovieId movieId, StreamingPlatform platform)
        {
            var movie = await _context.Movies
                .Include(m => m.StreamingPlatforms)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            movie.AddStreamingPlatform(platform);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlatform(StreamingPlatform platform)
        {
            var existingPlatform = await _context.StreamingPlatforms
                .FirstOrDefaultAsync(p => p.Id == platform.Id);

            if (existingPlatform == null)
                throw new KeyNotFoundException("Streaming platform not found");

            existingPlatform.Name = platform.Name;
            existingPlatform.Url = platform.Url;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePlatform(StreamingPlatform platform)
        {
            _context.StreamingPlatforms.Remove(platform);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlatformByMovieId(MovieId movieId, StreamingPlatformId platformId)
        {
            var movie = await _context.Movies
                .Include(m => m.StreamingPlatforms)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found");

            var platform = movie.StreamingPlatforms.FirstOrDefault(p => p.Id == platformId);
            if (platform != null)
            {
                movie.RemoveStreamingPlatform(platform);
                await _context.SaveChangesAsync();
            }
        }
    }
}
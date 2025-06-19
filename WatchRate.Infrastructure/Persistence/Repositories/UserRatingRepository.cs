using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class UserRatingRepository : IUserRatingRepository
    {
        private readonly WatchRateDbContext _context;

        public UserRatingRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRating>> GetByUserId(UserId userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRatings)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRatings ?? Enumerable.Empty<UserRating>();
        }

        public async Task AddRating(UserId userId, MovieId movieId, int value, string? review)
        {
            var user = await _context.Users
                .Include(u => u.UserRatings)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException("User not found");

            var rating = UserRating.Create(movieId, value, review, DateTime.UtcNow, null);
            await _context.UserRatings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRating(UserId userId, MovieId movieId, int value, string? review)
        {
            var rating = await _context.UserRatings
                .FirstOrDefaultAsync(r => r.MovieId == movieId);

            if (rating == null)
                throw new ArgumentException("Rating not found");

            rating = UserRating.Create(movieId, value, review, rating.CreatedDateTime, DateTime.UtcNow);
            _context.UserRatings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRating(UserId userId, MovieId movieId)
        {
            var rating = await _context.UserRatings
                .FirstOrDefaultAsync(r => r.MovieId == movieId);

            if (rating != null)
            {
                _context.UserRatings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
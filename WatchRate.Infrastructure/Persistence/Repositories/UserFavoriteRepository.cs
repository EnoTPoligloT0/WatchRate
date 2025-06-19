using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;
using WatchRate.Infrastucture.Persistence;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class UserFavoriteRepository : IUserFavoriteRepository
    {
        private readonly WatchRateDbContext _context;

        public UserFavoriteRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserFavorite>> GetByUserId(UserId userId)
        {
            var user = await _context.Users
                .Include(u => u.UserFavorites)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
                
            return user?.UserFavorites ?? Enumerable.Empty<UserFavorite>();
        }

        public async Task AddToFavorites(UserId userId, MovieId movieId)
        {
            // Retrieve the user along with current favorite items.
            var user = await _context.Users
                .Include(u => u.UserFavorites)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException("User not found");

            // Avoid adding duplicate favorites.
            if (user.UserFavorites.Any(f => f.MovieId.Equals(movieId)))
                return;

            // Create a new favorite using the factory method.
            var favorite = UserFavorite.Create(movieId, DateTime.UtcNow);
            // Set the shadow property "UserId" to ensure proper association.
            _context.Entry(favorite).Property("UserId").CurrentValue = userId;

            await _context.UserFavorites.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromFavorites(UserId userId, MovieId movieId)
        {
            // Find the favorite item by filtering on the shadow property "UserId" together with MovieId.
            var favorite = await _context.UserFavorites
                .FirstOrDefaultAsync(f => EF.Property<UserId>(f, "UserId").Equals(userId) &&
                                          f.MovieId.Equals(movieId));

            if (favorite != null)
            {
                _context.UserFavorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
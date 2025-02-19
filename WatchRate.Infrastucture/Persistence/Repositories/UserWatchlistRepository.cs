using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Domain.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class UserWatchlistRepository : IUserWatchlistRepository
    {
        private readonly WatchRateDbContext _context;

        public UserWatchlistRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserWatchlist>> GetByUserId(UserId userId)
        {
            var user = await _context.Users
                .Include(u => u.UserWatchlists)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
                
            return user?.UserWatchlists ?? Enumerable.Empty<UserWatchlist>();
        }

        public async Task AddToWatchlist(UserId userId, MovieId movieId)
        {
            var user = await _context.Users
                .Include(u => u.UserWatchlists)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new ArgumentException("User not found");

            if (user.UserWatchlists.Any(w => w.MovieId.Equals(movieId)))
                return;

            var watchlistItem = UserWatchlist.Create(movieId, DateTime.UtcNow);
            _context.Entry(watchlistItem).Property("UserId").CurrentValue = userId;

            await _context.UserWatchlists.AddAsync(watchlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromWatchlist(UserId userId, MovieId movieId)
        {
            var watchlistItem = await _context.UserWatchlists
                .FirstOrDefaultAsync(w => EF.Property<UserId>(w, "UserId").Equals(userId) &&
                                          w.MovieId.Equals(movieId));

            if (watchlistItem != null)
            {
                _context.UserWatchlists.Remove(watchlistItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Domain.UserAggregate;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WatchRateDbContext _context;

        public UserRepository(WatchRateDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(UserId id)
        {
            return await _context.Users
                .Include(u => u.UserWatchlists)
                .Include(u => u.UserFavorites)
                .Include(u => u.UserRatings)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.UserWatchlists)
                .Include(u => u.UserFavorites)
                .Include(u => u.UserRatings)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAll(int page = 1, int pageSize = 10)
        {
            return await _context.Users
                .Include(u => u.UserWatchlists)
                .Include(u => u.UserFavorites)
                .Include(u => u.UserRatings)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(UserId id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
} 
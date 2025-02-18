using WatchRate.Domain.UserAggregate;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetById(UserId id);
    Task<User?> GetByEmail(string email);
    Task<IEnumerable<User>> GetAll(int page = 1, int pageSize = 10);
    Task<User> Create(User user);
    void Update(User user);
    void Delete(UserId id);
} 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WatchRate.Infrastucture.Persistence;

namespace WatchRate.Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<WatchRateDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        return services;
    }
}

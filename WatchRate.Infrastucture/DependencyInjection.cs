using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WatchRate.Application.Common.Interfaces.Persistence;
using WatchRate.Application.Common.Interfaces.Persistence.Movie;
using WatchRate.Infrastucture.Persistence;
using WatchRate.Infrastucture.Persistence.Repositories;

namespace WatchRate.Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<WatchRateDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IMovieCastRepository, MovieCastRepository>();
        services.AddScoped<IMovieCrewRepository, MovieCrewRepository>();
        services.AddScoped<IMovieGenreRepository, MovieGenreRepository>();
        services.AddScoped<IStreamingPlatformRepository, StreamingPlatformRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserWatchlistRepository, UserWatchlistRepository>();
        services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
        services.AddScoped<IUserRatingRepository, UserRatingRepository>();
        
        return services;
    }
}

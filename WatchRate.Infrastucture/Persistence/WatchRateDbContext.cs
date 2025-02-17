using Microsoft.EntityFrameworkCore;
using WatchRate.Domain.Common.Attributes;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.Entities;
using WatchRate.Domain.PersonAggregate;
using WatchRate.Domain.StreamingAggregate;
using WatchRate.Domain.UserAggregate;
using WatchRate.Domain.UserAggregate.Entities;
using WatchRate.Infrastucture.Persistence.Extensions;

namespace WatchRate.Infrastucture.Persistence;

public class WatchRateDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<MovieCrew> MovieCrews { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<StreamingPlatform> StreamingPlatforms { get; set; }
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserWatchlist> UserWatchlists { get; set; } = null!;
    public DbSet<UserFavorite> UserFavorites { get; set; } = null!;
    public DbSet<UserRating> UserRatings { get; set; } = null!;
    
    public WatchRateDbContext(DbContextOptions<WatchRateDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WatchRateDbContext).Assembly);
        
        modelBuilder.AddStronglyTypedIdValueConverters<EfCoreValueConverterAttribute>();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchRate.Domain.MovieAggregate;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.PersonAggregate.ValueObjects;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Configurations;

public class MovieConfigurations : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        ConfigureMoviesTable(builder);
        ConfigureGenresTable(builder);
        ConfigureCastTable(builder);
        ConfigureCrewTable(builder);
        ConfigureStreamingUrlsTable(builder);
    }

    private void ConfigureMoviesTable(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

        // PK
        builder.HasKey(x => x.Id);

        // ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MovieId.Create(value));

        //DbId
        builder.Property(x => x.DbId)
            .HasMaxLength(20)
            .HasColumnName("ImdbId");  
        
        // Title
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        // Description
        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        // Year`
        builder.Property(x => x.Year)
            .IsRequired();

        // Runtime
        builder.Property(x => x.Runtime);

        // MaturityRating
        builder.Property(x => x.MaturityRating)
            .HasConversion<string>()
            .HasMaxLength(10);

        // AverageRating
        builder.Property(x => x.AverageRating)
            .HasPrecision(3, 1);

        // TotalRatings
        builder.Property(x => x.TotalRatings);

        // URLs
        builder.Property(x => x.PosterUrl)
            .HasMaxLength(500);
        builder.Property(x => x.BackdropUrl)
            .HasMaxLength(500);
        builder.Property(x => x.TrailerUrl)
            .HasMaxLength(500);

        // Timestamps
        builder.Property(x => x.CreatedDateTime)
            .IsRequired();
        builder.Property(x => x.UpdatedDateTime);
    }

    private void ConfigureGenresTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.MovieGenres, gb =>
        {
            gb.ToTable("MovieGenres");

            gb.WithOwner().HasForeignKey("MovieId");

            gb.HasKey("Id", "MovieId");

            gb.Property(g => g.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MovieGenreId.Create(value));

            gb.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
        });
        
        builder.Metadata.FindNavigation(nameof(Movie.MovieGenres))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureCastTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.MovieCasts, cb =>
        {
            cb.ToTable("MovieCast");

            cb.WithOwner().HasForeignKey("MovieId");

            cb.HasKey("Id", "MovieId");

            cb.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MovieCastId.Create(value));

            cb.Property(c => c.PersonId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => PersonId.Create(value));

            cb.Property(c => c.Character)
                .HasMaxLength(100);

            cb.Property(c => c.Order);
            
            cb.HasIndex(c => c.PersonId);
        });
        builder.Metadata.FindNavigation(nameof(Movie.MovieCasts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureCrewTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.MovieCrews, cb =>
        {
            cb.ToTable("MovieCrew");

            cb.WithOwner().HasForeignKey("MovieId");

            cb.HasKey("Id", "MovieId");

            cb.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MovieCrewId.Create(value));

            cb.Property(c => c.PersonId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => PersonId.Create(value));

            cb.Property(c => c.Role)
                .IsRequired()
                .HasMaxLength(50);

            cb.Property(c => c.Department)
                .HasMaxLength(50);
        });
        
        builder.Metadata.FindNavigation(nameof(Movie.MovieCrews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureStreamingUrlsTable(EntityTypeBuilder<Movie> builder)
    {
        builder.OwnsMany(m => m.StreamingPlatforms, sp =>
        {
            sp.ToTable("StreamingPlatforms");

            sp.WithOwner().HasForeignKey("MovieId");
            
            sp.HasKey("Id", "MovieId");

            sp.Property(i => i.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => StreamingPlatformId.Create(value));

            sp.Property(i => i.Name);
            sp.Property(i => i.Url);
        });
        
        builder.Metadata.FindNavigation(nameof(Movie.StreamingPlatforms))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchRate.Domain.MovieAggregate.ValueObjects;
using WatchRate.Domain.UserAggregate;
using WatchRate.Domain.UserAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistance.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureUserWatchlistTable(builder);
        ConfigureUserFavoritesTable(builder);
        ConfigureUserRatingsTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // PK
        builder.HasKey(x => x.Id);
        
        // ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        // Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(320);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        // Username
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(x => x.UserName)
            .IsUnique();

        // Password Hash
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        // Profile Picture URL
        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(500);

        // Timestamps
        builder.Property(x => x.CreatedDateTime)
            .IsRequired();
    }

    private void ConfigureUserWatchlistTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.UserWatchlists, wb =>
        {
            wb.ToTable("UserWatchlist");

            wb.WithOwner().HasForeignKey("UserId");

            wb.HasKey("Id", "UserId");

            wb.Property(w => w.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserWatchlistId.Create(value));

            wb.Property(w => w.MovieId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => MovieId.Create(value));

            wb.Property(w => w.CreatedDateTime)
                .IsRequired();
            
            wb.HasIndex("MovieId", "UserId")
                .IsUnique();
        });

        builder.Metadata.FindNavigation(nameof(User.UserWatchlists))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureUserFavoritesTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.UserFavorites, fb =>
        {
            fb.ToTable("UserFavorites");

            fb.WithOwner().HasForeignKey("UserId");

            fb.HasKey("Id", "UserId");

            fb.Property(f => f.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserFavoriteId.Create(value));

            fb.Property(f => f.MovieId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => MovieId.Create(value));

            fb.Property(f => f.AddedDateTime)
                .IsRequired();
        });

        builder.Metadata.FindNavigation(nameof(User.UserFavorites))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureUserRatingsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.UserRatings, rb =>
        {
            rb.ToTable("UserRatings");

            rb.WithOwner().HasForeignKey("UserId");

            rb.HasKey("Id", "UserId");

            rb.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserRatingId.Create(value));

            rb.Property(r => r.MovieId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => MovieId.Create(value));

            rb.Property(r => r.Value);

            rb.Property(r => r.Review)
                .HasMaxLength(2000);

            rb.Property(r => r.CreatedDateTime)
                .IsRequired();

            rb.Property(r => r.UpdatedDateTime);
        });

        builder.Metadata.FindNavigation(nameof(User.UserRatings))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WatchRate.Infrastucture.Persistance;
using WatchRate.Infrastucture.Persistence;

#nullable disable

namespace WatchRate.Infrastucture.Persistance.Migrations
{
    [DbContext(typeof(WatchRateDbContext))]
    partial class WatchRateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WatchRate.Domain.MovieAggregate.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("AverageRating")
                        .HasPrecision(3, 1)
                        .HasColumnType("numeric(3,1)");

                    b.Property<string>("BackdropUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DbId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("ImdbId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("MaturityRating")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("PosterUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<short?>("Runtime")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int?>("TotalRatings")
                        .HasColumnType("integer");

                    b.Property<string>("TrailerUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<short>("Year")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("WatchRate.Domain.PersonAggregate.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Biography")
                        .HasColumnType("text");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("WatchRate.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ProfilePictureUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WatchRate.Domain.MovieAggregate.Movie", b =>
                {
                    b.OwnsMany("WatchRate.Domain.MovieAggregate.Entities.MovieCast", "MovieCasts", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Character")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<string>("Order")
                                .HasColumnType("text");

                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id", "MovieId");

                            b1.HasIndex("MovieId");

                            b1.HasIndex("PersonId");

                            b1.ToTable("MovieCast", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MovieId");
                        });

                    b.OwnsMany("WatchRate.Domain.MovieAggregate.MovieCrew", "MovieCrews", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Department")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Role")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.HasKey("Id", "MovieId");

                            b1.HasIndex("MovieId");

                            b1.ToTable("MovieCrew", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MovieId");
                        });

                    b.OwnsMany("WatchRate.Domain.MovieAggregate.MovieGenre", "MovieGenres", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.HasKey("Id", "MovieId");

                            b1.HasIndex("MovieId");

                            b1.ToTable("MovieGenres", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MovieId");
                        });

                    b.OwnsMany("WatchRate.Domain.StreamingAggregate.StreamingPlatform", "StreamingPlatforms", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("Id", "MovieId");

                            b1.HasIndex("MovieId");

                            b1.ToTable("StreamingPlatforms", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MovieId");
                        });

                    b.Navigation("MovieCasts");

                    b.Navigation("MovieCrews");

                    b.Navigation("MovieGenres");

                    b.Navigation("StreamingPlatforms");
                });

            modelBuilder.Entity("WatchRate.Domain.UserAggregate.User", b =>
                {
                    b.OwnsMany("WatchRate.Domain.UserAggregate.Entities.UserFavorite", "UserFavorites", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("AddedDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id", "UserId");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserFavorites", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("WatchRate.Domain.UserAggregate.Entities.UserRating", "UserRatings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Review")
                                .HasMaxLength(2000)
                                .HasColumnType("character varying(2000)");

                            b1.Property<DateTime?>("UpdatedDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int?>("Value")
                                .HasColumnType("integer");

                            b1.HasKey("Id", "UserId");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserRatings", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("WatchRate.Domain.UserAggregate.Entities.UserWatchlist", "UserWatchlists", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("CreatedDateTime")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("UserWatchlist", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("UserFavorites");

                    b.Navigation("UserRatings");

                    b.Navigation("UserWatchlists");
                });
#pragma warning restore 612, 618
        }
    }
}

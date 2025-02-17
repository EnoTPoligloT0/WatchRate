using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchRate.Domain.PersonAggregate;
using WatchRate.Domain.PersonAggregate.ValueObjects;

namespace WatchRate.Infrastucture.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");

        builder.HasKey("Id");
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PersonId.Create(value));
        
        builder.Property(x => x.FirstName)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .IsRequired();
        
        builder.Property(x => x.Biography);
        
        builder.Property(x => x.BirthDate)
            .IsRequired();
        
        builder.Property(x => x.BirthPlace)
            .IsRequired();
        
        builder.Property(x => x.ProfileImageUrl);
        
        builder.Property(x => x.CreatedDateTime)
            .IsRequired();
        
        builder.Property(x => x.UpdatedDateTime);
    }
}
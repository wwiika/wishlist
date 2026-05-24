using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishList.Domain.Entities;
using WishList.Domain.Enums;
using WishList.ValueObjects;

namespace WishList.Infrastructure.EntityFramework.Configurations;

public class GiftConfiguration : IEntityTypeConfiguration<Gift>
{
    public void Configure(EntityTypeBuilder<Gift> builder)
    {
        builder.ToTable(nameof(Gift));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasConversion(
                x => x.Value,
                x => new Title(x))
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasConversion(
                x => x == null ? null : x.Value,
                x => x == null ? null : new Link(x));

        builder.Property(x => x.Price)
            .HasConversion(
                x => x.Value,
                x => new Price(x))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Gifts);

        builder.HasMany(x => x.Reservations)
            .WithOne(x => x.Gift);

        builder.HasOne(x => x.CurrentReservation);

        builder.Navigation(x => x.Reservations)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
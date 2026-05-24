using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishList.Domain.Entities;
using WishList.ValueObjects;

namespace WishList.Infrastructure.EntityFramework.Configurations;

public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable(nameof(Friend));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.FriendId)
            .HasConversion(
                x => x.Value,
                x => new UserId(x))
            .IsRequired();

        builder.Property(x => x.Username)
            .HasConversion(
                x => x.Value,
                x => new Username(x))
            .HasMaxLength(30)
            .IsRequired();

        builder.HasMany(x => x.Reservations)
            .WithOne(x => x.Friend);

        builder.Navigation(x => x.Reservations)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
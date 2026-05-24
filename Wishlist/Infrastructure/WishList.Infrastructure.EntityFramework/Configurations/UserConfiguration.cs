using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishList.Domain.Entities;
using WishList.ValueObjects;

namespace WishList.Infrastructure.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
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

        builder.HasMany(x => x.Gifts)
            .WithOne(x => x.User);

        builder.Navigation(x => x.Gifts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.Username).HasColumnType("varchar(50)").IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Email).HasColumnType("varchar(255)").IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password).HasColumnType("varchar(100)").IsRequired();
        builder.Property(u => u.Phone).HasColumnType("varchar(20)");
        builder.HasIndex(u => u.Phone); 
        builder.Property(u => u.Status).HasColumnType("varchar(15)").HasConversion<string>().IsRequired();
        builder.Property(u => u.Role).HasColumnType("varchar(15)").HasConversion<string>().IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnType("timestamp with time zone");
    }
}

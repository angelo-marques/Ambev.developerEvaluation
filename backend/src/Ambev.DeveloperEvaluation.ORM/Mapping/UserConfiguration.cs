using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("users");
        // Primary Key
        builder.HasKey(u => u.Id);

        // Configure ID as UUID with default value
        builder.Property(u => u.Id)
               .HasColumnType("uuid")
               .HasDefaultValueSql("gen_random_uuid()");

        // Username
        builder.Property(u => u.Username)
               .HasColumnType("varchar(50)")
               .IsRequired();
        builder.HasIndex(u => u.Username).IsUnique();

        // Email
        builder.Property(u => u.Email)
               .HasColumnType("varchar(255)")
               .IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();

        // Password
        builder.Property(u => u.Password)
               .HasColumnType("varchar(100)")
               .IsRequired();

        // Phone
        builder.Property(u => u.Phone)
               .HasColumnType("varchar(20)");
        builder.HasIndex(u => u.Phone); // Não único, mas otimiza busca

        // Status
        builder.Property(u => u.Status)
               .HasColumnType("varchar(15)")
               .HasConversion<string>()
               .IsRequired();

        // Role
        builder.Property(u => u.Role)
               .HasColumnType("varchar(15)")
               .HasConversion<string>()
               .IsRequired();

        // CreatedAt e UpdatedAt
        builder.Property(u => u.CreatedAt)
               .HasColumnType("timestamp with time zone")
               .IsRequired();

        builder.Property(u => u.UpdatedAt)
               .HasColumnType("timestamp with time zone");

   

        //builder.ToTable("Users");

        //builder.HasKey(u => u.Id);
        //builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        //builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        //builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        //builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        //builder.Property(u => u.Phone).HasMaxLength(20);

        //builder.Property(u => u.Status)
        //    .HasConversion<string>()
        //    .HasMaxLength(20);

        //builder.Property(u => u.Role)
        //    .HasConversion<string>()
        //    .HasMaxLength(20);

    }
}

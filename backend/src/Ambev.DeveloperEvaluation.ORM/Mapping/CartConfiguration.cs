using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.UserId)
                   .HasColumnType("uuid")
                   .IsRequired();

            builder.Property(c => c.Date)
                   .HasColumnType("timestamp with time zone")
                   .IsRequired();

            builder.HasIndex(c => c.UserId);

            builder.OwnsMany(c => c.Products, cartItems =>
            {
                cartItems.Property(ci => ci.ProductId)
                        .HasColumnName("ProductId")
                        .HasColumnType("uuid")
                        .IsRequired();

                cartItems.Property(ci => ci.Quantity)
                        .HasColumnName("Quantity")
                        .HasColumnType("int")
                        .IsRequired();
            });
        }
    }
}

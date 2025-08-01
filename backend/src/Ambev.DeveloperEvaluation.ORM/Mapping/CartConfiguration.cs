using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).HasColumnName("user_id");
            builder.Property(c => c.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
           // builder.Property(c => c.Products).HasColumnName("products");
            builder.Property(c => c.Date).HasColumnName("date");
            builder.Property(c => c.PriceTotal).HasColumnName("price_total");

            builder.OwnsMany(c => c.Products, cartItems =>
            {
                cartItems.Property(ci => ci.ProductId)
                        .HasColumnName("product_id")
                        .HasColumnType("uuid")
                        .IsRequired();

                cartItems.Property(ci => ci.Quantity)
                        .HasColumnName("quantity")
                        .HasColumnType("int")
                        .IsRequired();
            });
        }
    }
}

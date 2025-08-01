using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            builder.ToTable("cart_items");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(ci => ci.ProductId).HasColumnName("product_id");          
            builder.Property(ci => ci.Quantity).HasColumnName("quantity");
            builder.Property(ci => ci.Discount).HasColumnName("discont").HasColumnType("decimal(8,2)"); 
            builder.Property(ci => ci.PriceTotal).HasColumnName("price_total").HasColumnType("decimal(8,2)");
            builder.Property(ci => ci.PriceTotalWithDiscount).HasColumnName("price_total_discount").HasColumnType("decimal(8,2)");
            builder.Property(ci => ci.UnitPrice).HasColumnName("unit_price").IsRequired().HasColumnType("decimal(10,2)"); 
        }
    }
}

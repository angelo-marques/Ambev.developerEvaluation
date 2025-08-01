using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(p => p.Title).HasColumnName("title").HasColumnType("varchar(100)").IsRequired();
            builder.HasIndex(p => p.Title);
            builder.Property(p => p.Price).HasColumnName("price").HasColumnType("numeric(10,2)").IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").HasColumnType("text");
            builder.Property(p => p.Image).HasColumnName("image").HasColumnType("text");

            builder.OwnsOne(p => p.Category, category =>
            {
                category.Property(c => c.ExternalId).HasColumnName("category_external_id").HasColumnType("varchar(50)"); 
                category.Property(c => c.Name).HasColumnName("category_name").HasColumnType("varchar(100)");
            });
           
            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.ExternalId).HasColumnName("rating_external_id").HasColumnType("varchar(50)");
                rating.Property(r => r.AverageRate).HasColumnName("rating_average_rate").HasColumnType("numeric(3,1)");
                rating.Property(r => r.TotalReviews).HasColumnName("rating_total_reviews").HasColumnType("int");
            });
        }
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            
            builder.HasKey(p => p.Id);
          
            builder.Property(p => p.Id)
                   .HasColumnType("uuid")
                   .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(p => p.Title)
                   .HasColumnType("varchar(100)")
                   .IsRequired();
            builder.HasIndex(p => p.Title);

            builder.Property(p => p.Price)
                   .HasColumnType("numeric(10,2)")
                   .IsRequired();
         
            builder.Property(p => p.Description)
                   .HasColumnType("text");

            builder.Property(p => p.Image)
                   .HasColumnType("text");

            builder.OwnsOne(p => p.Category, category =>
            {
                category.Property(c => c.ExternalId)
                        .HasColumnName("Category_ExternalId")
                        .HasColumnType("varchar(50)"); 

                category.Property(c => c.Name)
                        .HasColumnName("Category_Name")
                        .HasColumnType("varchar(100)");
            });
           
            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.ExternalId)
                      .HasColumnName("Rating_ExternalId")
                      .HasColumnType("varchar(50)");

                rating.Property(r => r.AverageRate)
                      .HasColumnName("Rating_AverageRate")
                      .HasColumnType("numeric(3,1)");

                rating.Property(r => r.TotalReviews)
                      .HasColumnName("Rating_TotalReviews")
                      .HasColumnType("int");
            });
        }
    }
}

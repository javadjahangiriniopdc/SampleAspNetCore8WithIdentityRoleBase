using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAspNetCore8WithIdentityRoleBase.Models;

namespace SampleAspNetCore8WithIdentityRoleBase.Data.EntityConfig
{
    public class ProductConfig:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);


            builder
                .Property(x => x.Id)
                .HasColumnName("ProductId")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Name).IsDescending();

        }
    }
}

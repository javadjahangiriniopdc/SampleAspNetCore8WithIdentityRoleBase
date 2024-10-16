using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAspNetCore8WithIdentityRoleBase.Models;

namespace SampleAspNetCore8WithIdentityRoleBase.Data.EntityConfig
{
    public class OrderAppConfig:IEntityTypeConfiguration<OrderApp>
    {
        public void Configure(EntityTypeBuilder<OrderApp> builder)
        {
            builder.ToTable("OrderApps");
            builder.HasKey(x => x.Id);


            builder
                .Property(x => x.Id)
                .HasColumnName("OrderAppId")
                .ValueGeneratedOnAdd();


            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerID)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProductID)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

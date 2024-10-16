using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleAspNetCore8WithIdentityRoleBase.Models;

namespace SampleAspNetCore8WithIdentityRoleBase.Data.EntityConfig
{
    public class CustomerConfig:IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);


            builder
                .Property(x => x.Id)
                .HasColumnName("CustomerId")
                .ValueGeneratedOnAdd();


            builder.HasIndex(x => x.Email).IsUnique();

        }
    }
}

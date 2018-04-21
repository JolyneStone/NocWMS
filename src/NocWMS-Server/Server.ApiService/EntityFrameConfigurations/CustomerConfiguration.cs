using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class CustomerConfiguration : EntityCommonConfiguration<Customer>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customers");

            entity.HasKey(customer => customer.Id);
            //entity.Property(customer => customer.Id)
            //    .HasMaxLength(30)
            //    .ValueGeneratedNever();

            entity.HasAlternateKey(customer => customer.CustomerName);

            entity.Property(customer => customer.CustomerName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(customer => customer.Contact)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(customer => customer.PostCode)
                .HasMaxLength(20);

            entity.Property(customer => customer.Telephone)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(customer => customer.Email)
               .IsRequired()
               .HasMaxLength(50);

            entity.Property(customer => customer.Duty)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(customer => customer.Fax)
                .HasMaxLength(11);

            entity.Property(customer => customer.Gender)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasDefaultValueSql("0"); 

            entity.Property(customer => customer.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(customer => customer.Remarks)
                .HasMaxLength(255);

            entity.Property(customer => customer.CreateTime)
          .HasColumnType("datetime")
          .HasDefaultValueSql("getdate()");

            entity.HasMany(customer => customer.OutboundReceipts)
                .WithOne(outboundReceipt => outboundReceipt.Customer)
                .HasForeignKey(outboundReceipt => outboundReceipt.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

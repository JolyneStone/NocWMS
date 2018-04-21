using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class VendorConfiguration : EntityCommonConfiguration<Vendor>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Vendor> entity)
        {
            entity.ToTable("Vendors");

            entity.HasAlternateKey(vendor => vendor.VendorName);

            entity.HasKey(vendor => vendor.Id);
            //entity.Property(vendor => vendor.Id)
            //    .HasMaxLength(30)
            //    .ValueGeneratedNever();

            entity.Property(vendor => vendor.VendorName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(vendor => vendor.Contact)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(vendor => vendor.PostCode)
                .HasMaxLength(20);

            entity.Property(vendor => vendor.Telephone)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(vendor => vendor.Email)
               .IsRequired()
               .HasMaxLength(50);

            entity.Property(vendor => vendor.Duty)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(vendor => vendor.Fax)
                .HasMaxLength(11);

            entity.Property(vendor => vendor.Gender)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasDefaultValueSql("0");

            entity.Property(vendor => vendor.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(vendor => vendor.Remarks)
                .HasMaxLength(255);

            entity.Property(vendor => vendor.CreateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.HasMany(vendor=>vendor.InboundReceipts)
                .WithOne(inboundReceipt => inboundReceipt.Vendor)
                .HasForeignKey(inboundReceipt => inboundReceipt.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

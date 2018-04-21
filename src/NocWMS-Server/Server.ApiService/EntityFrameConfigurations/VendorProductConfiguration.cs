using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class VendorProductConfiguration : EntityCommonConfiguration<VendorProduct>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<VendorProduct> entity)
        {
            entity.ToTable("Vendor_Products");

            entity.HasKey(vendorProduct => new { vendorProduct.ProductId, vendorProduct.VendorId });

            entity.HasOne(vendorProduct => vendorProduct.Vendor)
                .WithMany(vendor => vendor.VendorProducts)
                .HasForeignKey(vendorProduct => vendorProduct.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(vendorProduct => vendorProduct.Product)
                .WithMany(vendorProduct => vendorProduct.VendorProducts)
                .HasForeignKey(vendorProduct => vendorProduct.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

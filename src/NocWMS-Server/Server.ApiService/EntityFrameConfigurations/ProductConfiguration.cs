using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class ProductConfiguration : EntityConfiguration<Product, int>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");

            entity.HasKey(product => product.Id);

            entity.HasAlternateKey(product => product.ProductName);

            entity.Property(product => product.ProductName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(product => product.Model)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(product => product.SellPrice)
                .IsRequired();

            entity.Property(product => product.Spec)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(product => product.Unit)
                .IsRequired()
                .HasMaxLength(10);

            entity.HasMany(product => product.InboundReceiptItems)
                .WithOne(inboundReceiptItem => inboundReceiptItem.Product)
                .HasForeignKey(inboundReceiptItem => inboundReceiptItem.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(product => product.OutboundReceiptItems)
                .WithOne(outboundReceiptItem => outboundReceiptItem.Product)
                .HasForeignKey(outboundReceiptItem => outboundReceiptItem.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

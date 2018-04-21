using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class CategoryConfiguration : EntityConfiguration<Category, int>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Categories");

            entity.HasKey(category => category.Id);

            entity.HasAlternateKey(category => category.CategoryName);

            entity.Property(category => category.CategoryName)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(category => category.Creator)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(category => category.Remarks)
                .HasMaxLength(255);

            entity.Property(category => category.CreateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(category => category.InboundReceiptItems)
                .WithOne(inboundReceipt => inboundReceipt.Category)
                .HasForeignKey(inboundReceipt => inboundReceipt.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(category => category.OutboundReceiptItems)
                .WithOne(outboundReceipt => outboundReceipt.Category)
                .HasForeignKey(outboundReceipt => outboundReceipt.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

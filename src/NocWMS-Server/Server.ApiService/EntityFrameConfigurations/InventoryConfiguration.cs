using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class InventoryConfiguration : EntityConfiguration<Inventory, int>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Inventory> entity)
        {
            entity.ToTable("Inventories");

            entity.HasKey(inventory => inventory.Id);

            entity.Property(inventory => inventory.RealInventory)
                .IsRequired();

            entity.Property(inventory => inventory.BookInventory)
                .IsRequired();

            entity.HasOne(inventory => inventory.Product)
                .WithMany(product => product.Inventories)
                .HasForeignKey(inventory => inventory.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(inventory => inventory.Category)
                .WithMany(category => category.Inventories)
                .HasForeignKey(inventory => inventory.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(inventory => inventory.Warehouse)
                .WithMany(warehouse => warehouse.Inventories)
                .HasForeignKey(inventory => inventory.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(inventory => inventory.InboundReceiptItems)
            //    .WithOne(inboundReceiptItem => inboundReceiptItem.Inventory)
            //    .HasForeignKey(inboundReceiptItem => inboundReceiptItem.InventoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(inventory => inventory.OutboundReceiptItems)
            //  .WithOne(outboundReceiptItem => outboundReceiptItem.Inventory)
            //  .HasForeignKey(outboundReceiptItem => outboundReceiptItem.InventoryId)
            //  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

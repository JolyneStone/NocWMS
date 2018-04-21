using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class InventoryCellConfiguration : EntityCommonConfiguration<InventoryCell>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<InventoryCell> entity)
        {
            entity.ToTable("Inventory_Cells");

            entity.HasKey(inventoryCell => new { inventoryCell.Id, inventoryCell.InventoryId });

            entity.Property(inventoryCell => inventoryCell.Num);

            entity.Property(inventoryCell => inventoryCell.UpdateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.Property(inventoryCell => inventoryCell.ReceiptType)
                 .IsRequired()
                 .HasColumnType("TINYINT");

            entity.Property(inventoryCell => inventoryCell.ReceiptId)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(inventoryCell => inventoryCell.Inventory)
                .WithMany(inventory => inventory.InventoryCells)
                .HasForeignKey(inventoryCell => inventoryCell.InventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(inventoryCell => inventoryCell.WarehouseCell)
               .WithMany(warehouseCell => warehouseCell.InventoryCells)
               .HasForeignKey(inventoryCell => inventoryCell.Id)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

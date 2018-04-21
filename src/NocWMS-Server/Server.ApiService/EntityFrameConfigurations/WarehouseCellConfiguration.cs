using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class WarehouseCellConfiguration : EntityConfiguration<WarehouseCell, int>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<WarehouseCell> entity)
        {
            entity.ToTable("WarehouseCells");

            entity.HasKey(warehouseCell => warehouseCell.Id);

            entity.Property(warehouseCell => warehouseCell.CellName)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(warehouseCell => warehouseCell.RemainderVolume)
                .IsRequired()
                .HasDefaultValueSql("0");

            entity.Property(warehouseCell => warehouseCell.Volume)
                .IsRequired();

            entity.Property(warehouseCell=>warehouseCell.Status)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasDefaultValueSql("0");

            entity.HasMany(warehouseCell => warehouseCell.InboundReceiptItems)
                .WithOne(inboundReceiptItem => inboundReceiptItem.WarehouseCell)
                .HasForeignKey(inboundReceiptItem => inboundReceiptItem.WarehouseCellId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(warehouseCell => warehouseCell.OutboundReceiptItems)
                .WithOne(outboundReceiptItem => outboundReceiptItem.WarehouseCell)
                .HasForeignKey(outboundReceiptItem => outboundReceiptItem.WarehouseCellId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

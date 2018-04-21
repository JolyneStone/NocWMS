using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class WarehouseConfiguration : EntityCommonConfiguration<Warehouse>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Warehouse> entity)
        {
            entity.ToTable("Warehouses");

            entity.HasKey(warehouse => warehouse.Id);

            entity.HasAlternateKey(warehouse => warehouse.WarehouseName);

            entity.Property(warehouse => warehouse.WarehouseName)
                .IsRequired()
                .HasMaxLength(30);

            //entity.Property(warehouse => warehouse.StaffId)
            //    .HasMaxLength(30);

            entity.Property(warehouse => warehouse.Province)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(warehouse => warehouse.Address)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(warehouse => warehouse.Remarks)
                .HasMaxLength(255);

            entity.Property(warehouse => warehouse.CreateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            entity.HasMany(warehouse => warehouse.Cells)
                .WithOne(warehouseCell => warehouseCell.Warehouse)
                .HasForeignKey(warehouseCell => warehouseCell.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(warehouse => warehouse.InboundReceipts)
                .WithOne(inboundReceipt => inboundReceipt.Warehouse)
                .HasForeignKey(inboundReceipt => inboundReceipt.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(warehouse => warehouse.OutboundReceipts)
                .WithOne(outboundReceipt => outboundReceipt.Warehouse)
                .HasForeignKey(outboundReceipt => outboundReceipt.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

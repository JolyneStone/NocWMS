using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class StaffConfiguration:EntityCommonConfiguration<Staff>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<Staff> entity)
        {
            entity.ToTable("Staffs");

            entity.HasKey(staff => staff.Id);

            //entity.Property(staff => staff.Id)
            //     .HasMaxLength(30)
            //     .ValueGeneratedNever();

            entity.HasAlternateKey(staff => staff.Email); // 备用键即唯一约束/唯一键

            entity.Property(staff => staff.StaffName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(staff => staff.Telephone)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(staff => staff.Email)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(staff => staff.Duty)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(staff => staff.QQNumber)
                .HasMaxLength(16);

            entity.Property(staff => staff.Gender)
                .IsRequired()
                .HasColumnType("TINYINT")
                .HasDefaultValueSql("0");

            entity.Property(staff => staff.Remarks)
                .HasMaxLength(255);

            entity.Property(staff => staff.CreateTime)
              .HasColumnType("datetime")
              .HasDefaultValueSql("getdate()");

            entity.HasMany(staff => staff.InboundReceipts)
                .WithOne(inboundReceipt => inboundReceipt.Staff)
                .HasForeignKey(inboundReceipt => inboundReceipt.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(staff => staff.OutboundReceipts)
                .WithOne(outboundReceipt => outboundReceipt.Staff)
                .HasForeignKey(outboundReceipt => outboundReceipt.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(staff => staff.Warehouses)
                .WithOne(warehouse => warehouse.Staff)
                .HasForeignKey(warehouse => warehouse.StaffId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

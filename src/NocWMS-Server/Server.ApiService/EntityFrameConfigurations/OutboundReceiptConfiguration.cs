using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class OutboundReceiptConfiguration : EntityCommonConfiguration<OutboundReceipt>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<OutboundReceipt> entity)
        {
            entity.ToTable("OutboundReceipts");

            entity.HasKey(outboundReceipt => outboundReceipt.Id);

            //entity.Property(outboundReceipt => outboundReceipt.Id)
            //     .HasMaxLength(30)
            //     .ValueGeneratedNever();

            //entity.Property(outboundReceipt => outboundReceipt.CustomerId)
            //    .HasMaxLength(30);

            //entity.Property(outboundReceipt => outboundReceipt.StaffId)
            //    .HasMaxLength(30);

            entity.Property(outboundReceipt => outboundReceipt.Acceptor)
                .HasMaxLength(50);

            entity.Property(outboundReceipt => outboundReceipt.Deliveryman)
                .HasMaxLength(50);

            entity.Property(outboundReceipt => outboundReceipt.HandlerName)
                .HasMaxLength(50);

            entity.Property(outboundReceipt => outboundReceipt.WaybillNo)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(outboundReceipt => outboundReceipt.IsDone)
                .IsRequired()
                .HasDefaultValueSql("0");

            entity.Property(outboundReceipt => outboundReceipt.Total)
                .IsRequired()
                .HasDefaultValueSql("0");

            entity.Property(outboundReceipt => outboundReceipt.CreateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}

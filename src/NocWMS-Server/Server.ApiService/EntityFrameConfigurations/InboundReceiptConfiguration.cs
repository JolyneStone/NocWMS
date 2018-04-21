using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class InboundReceiptConfiguration : EntityCommonConfiguration<InboundReceipt>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<InboundReceipt> entity)
        {
            entity.ToTable("InboundReceipts");

            entity.HasKey(inboundReceipt => inboundReceipt.Id);

            //entity.Property(inboundReceipt => inboundReceipt.Id)
            //    .HasMaxLength(30)
            //    .ValueGeneratedNever();

            //entity.Property(inboundReceipt => inboundReceipt.VendorId)
            //    .HasMaxLength(30);

            //entity.Property(inboundReceipt => inboundReceipt.StaffId)
            //    .HasMaxLength(30);

            entity.Property(inboundReceipt => inboundReceipt.Acceptor)
                .HasMaxLength(50);

            entity.Property(inboundReceipt => inboundReceipt.Deliveryman)
                .HasMaxLength(50);

            entity.Property(inboundReceipt => inboundReceipt.HandlerName)
                .HasMaxLength(50);

            entity.Property(inboundReceipt => inboundReceipt.WaybillNo)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(inboundReceipt => inboundReceipt.IsDone)
                .IsRequired()
                .HasDefaultValueSql("0");

            entity.Property(inboundReceipt => inboundReceipt.CreateTime)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");
        }
    }
}

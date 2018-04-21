using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class OutboundReceiptItemConfiguration : EntityConfiguration<OutboundReceiptItem>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<OutboundReceiptItem> entity)
        {
            entity.ToTable("OutboundReceiptItems");

            entity.HasKey(outboundReceiptItem => outboundReceiptItem.Id);

            entity.Property(outboundReceiptItem => outboundReceiptItem.Num)
                .IsRequired();

            entity.Property(outboundReceiptItem => outboundReceiptItem.Price)
                .IsRequired();

            entity.HasOne(outboundReceiptIntem => outboundReceiptIntem.OutboundReceipt)
                .WithMany(outboundReceipt => outboundReceipt.OutboundReceiptItems)
                .HasForeignKey(outboundReceiptItem => outboundReceiptItem.OutboundReceiptId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

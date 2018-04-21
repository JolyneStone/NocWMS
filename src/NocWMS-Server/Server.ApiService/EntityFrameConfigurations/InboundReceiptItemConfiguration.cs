using KiraNet.Camellia.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ApiService.Models;

namespace Server.ApiService.EntityFrameConfigurations
{
    public class InboundReceiptItemConfiguration : EntityConfiguration<InboundReceiptItem>
    {
        protected override void ConfigureDerived(EntityTypeBuilder<InboundReceiptItem> entity)
        {
            entity.ToTable("InboundReceiptItems");

            entity.HasKey(inboundReceiptItem => inboundReceiptItem.Id);

            entity.Property(inboundReceiptItem => inboundReceiptItem.Num)
                .IsRequired();

            entity.Property(inboundReceiptItem => inboundReceiptItem.Price)
                .IsRequired();

            entity.HasOne(inboundReceiptIntem => inboundReceiptIntem.InboundReceipt)
                .WithMany(inboundReceipt => inboundReceipt.InboundReceiptItems)
                .HasForeignKey(inboundReceiptItem => inboundReceiptItem.InboundReceiptId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

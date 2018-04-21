using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class InboundReceiptItem : IEntity
    {
        public string Id { get; set; }
        public string InboundReceiptId { get; set; }
        public int WarehouseCellId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int Num { get; set; }
        public float Price { get; set; }

        public Product Product { get; set; }
        public Category Category { get; set; }
        public InboundReceipt InboundReceipt { get; set; }
        public WarehouseCell WarehouseCell { get; set; }
    }
}

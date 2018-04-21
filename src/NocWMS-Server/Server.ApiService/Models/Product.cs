using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string Model { get; set; }
        public float SellPrice { get; set; }

        public List<Inventory> Inventories { get; set; }
        public List<InboundReceiptItem> InboundReceiptItems { get; set; }
        public List<OutboundReceiptItem> OutboundReceiptItems { get; set; }
        public List<VendorProduct> VendorProducts { get; set; }
        public Category Category { get; set; }
        public List<InventoryCell> InventoryCells { get; set; }
    }
}

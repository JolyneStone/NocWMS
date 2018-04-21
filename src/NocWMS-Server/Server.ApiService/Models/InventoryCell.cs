using System;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;

namespace Server.ApiService.Models
{
    public class InventoryCell : IEntity<int>
    {
        public int Id { get; set; } // WarehouseCellId
        public int InventoryId { get; set; }
        public int Num { get; set; }
        public DateTime UpdateTime { get; set; }
        public ReceiptType ReceiptType { get; set; }
        public string ReceiptId { get; set; }
        public Inventory Inventory { get; set; }
        public WarehouseCell WarehouseCell { get; set; }
    }
}

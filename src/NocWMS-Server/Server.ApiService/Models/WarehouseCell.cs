using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;

namespace Server.ApiService.Models
{
    public class WarehouseCell : IEntity<int>
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public string CellName { get; set; }
        public int Volume { get; set; }
        public int RemainderVolume { get; set; }
        public CellStatus Status { get; set; } = CellStatus.Available;

        public Warehouse Warehouse { get; set; }
        public List<InventoryCell> InventoryCells { get; set; }
        public List<InboundReceiptItem> InboundReceiptItems { get; set; }
        public List<OutboundReceiptItem> OutboundReceiptItems { get; set; }
    }
}

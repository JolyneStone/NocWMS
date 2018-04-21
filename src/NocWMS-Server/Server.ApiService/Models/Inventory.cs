using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class Inventory : IEntity<int>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int BookInventory { get; set; }
        public int RealInventory { get; set; }

        public Warehouse Warehouse { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
        public List<InventoryCell> InventoryCells { get; set; }
    }
}

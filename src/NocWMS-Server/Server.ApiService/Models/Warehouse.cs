using System;
using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class Warehouse : IEntity<int>
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string StaffId { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

        public List<WarehouseCell> Cells { get; set; }
        public List<InboundReceipt> InboundReceipts { get; set; }
        public List<OutboundReceipt> OutboundReceipts { get; set; }
        public List<Inventory> Inventories { get; set; }
        public Staff Staff { get; set; }
    }
}
using System;
using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class OutboundReceipt : IEntity
    {
        public string Id { get; set; }
        public int WarehouseId { get; set; }
        public string CustomerId { get; set; }
        public string WaybillNo { get; set; }
        public float Total { get; set; }
        public string StaffId { get; set; }
        public string HandlerName { get; set; }
        public string Acceptor { get; set; }
        public string Deliveryman { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateTime { get; set; }

        public Warehouse Warehouse { get; set; }
        public Staff Staff { get; set; }
        public Customer Customer { get; set; }
        public List<OutboundReceiptItem> OutboundReceiptItems { get; set; }
    }
}

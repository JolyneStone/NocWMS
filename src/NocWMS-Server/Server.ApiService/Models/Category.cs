using System;
using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;

namespace Server.ApiService.Models
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Creator { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

        public List<Inventory> Inventories { get; set; }
        public List<InboundReceiptItem> InboundReceiptItems { get; set; }
        public List<OutboundReceiptItem> OutboundReceiptItems { get; set; }
        public List<Product> Products { get; set; }
    }
}
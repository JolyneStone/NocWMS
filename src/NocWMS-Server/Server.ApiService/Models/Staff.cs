using System;
using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;

namespace Server.ApiService.Models
{
    public class Staff : IEntity
    {
        public string Id { get; set; }
        public string UserInfoId { get; set; }
        public string StaffName { get; set; }
        public Gender Gender { get; set; }
        public string Duty { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string QQNumber { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

        public UserInfo UserInfo { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<OutboundReceipt> OutboundReceipts { get; set; }
        public List<InboundReceipt> InboundReceipts { get; set; }
    }
}

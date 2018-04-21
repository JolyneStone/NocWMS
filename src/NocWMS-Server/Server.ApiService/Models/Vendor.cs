using System;
using System.Collections.Generic;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Common;

namespace Server.ApiService.Models
{
    public class Vendor : IEntity
    {
        public string Id { get; set; }
        public string VendorName { get; set; }
        public Gender Gender { get; set; } = Gender.Male;
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Duty { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

        public List<InboundReceipt> InboundReceipts { get; set; }
        public List<VendorProduct> VendorProducts { get; set; }
    }
}

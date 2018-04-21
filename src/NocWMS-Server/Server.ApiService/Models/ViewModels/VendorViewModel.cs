using System;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class VendorViewModel
    {
        public string VendorName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Duty { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

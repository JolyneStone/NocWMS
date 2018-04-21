using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class VendorDisplayViewModel
    {
        public string Id { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Gender Gender { get; set; }
        public string Duty { get; set; }
        public string Telephone { get; set; }
    }
}

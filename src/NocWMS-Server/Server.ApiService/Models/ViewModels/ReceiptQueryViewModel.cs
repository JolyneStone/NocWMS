using System;

namespace Server.ApiService.Models.ViewModels
{
    public class ReceiptQueryViewModel
    {
        public int WarehouseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

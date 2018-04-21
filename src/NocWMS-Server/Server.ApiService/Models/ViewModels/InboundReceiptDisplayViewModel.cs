namespace Server.ApiService.Models.ViewModels
{
    public class InboundReceiptDisplayViewModel
    {
        public string Id { get; set; }
        public string VendorName { get; set; }
        public string WarehouseName { get; set; }
        public string WaybillNo { get; set; }
        public bool IsDone { get; set; }
        public string CreateTime { get; set; }
    }
}

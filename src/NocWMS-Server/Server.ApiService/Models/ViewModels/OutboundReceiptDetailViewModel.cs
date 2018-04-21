namespace Server.ApiService.Models.ViewModels
{
    public class OutboundReceiptDetailViewModel
    {
        public string Id { get; set; }
        public string WarehouseName { get; set; }
        public string CustomerName { get; set; }
        public string WaybillNo { get; set; }
        public string HandlerName { get; set; }
        public string Acceptor { get; set; }
        public string Deliveryman { get; set; }
        public bool IsDone { get; set; }
        public string CreateTime { get; set; }

        public OutboundReceiptItemDisplayViewModel[] OutboundReceiptItems { get; set; }
    }
}

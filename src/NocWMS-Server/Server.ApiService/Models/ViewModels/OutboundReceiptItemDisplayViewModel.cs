namespace Server.ApiService.Models.ViewModels
{
    public class OutboundReceiptItemDisplayViewModel
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductUnit { get; set; }
        public string ProductSpec { get; set; }
        public string ProductModel { get; set; }
        public int Num { get; set; }
        public float ProductPrice { get; set; }
        public string StoreCell { get; set; }
    }
}

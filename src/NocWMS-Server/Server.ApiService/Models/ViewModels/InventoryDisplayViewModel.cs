namespace Server.ApiService.Models.ViewModels
{
    public class InventoryDisplayViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public float ProductPrice { get; set; }
        public string CategoryName { get; set; }
        public string WarehouseName { get; set; }
        public int BookInventory { get; set; }
        public int RealInventory { get; set; }
    }
}

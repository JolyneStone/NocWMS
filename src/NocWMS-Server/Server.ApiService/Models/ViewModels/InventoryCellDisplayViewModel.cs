using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class InventoryCellDisplayViewModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string CellName { get; set; }
        public string UpdateTime { get; set; }
        public ReceiptType ReceiptType { get; set; }
        public string ReceiptId { get; set; }
        public int Num { get; set; }
    }
}

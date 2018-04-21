using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class WarehouseCellDisplayViewModel
    {
        public int Id { get; set; }
        public string CellName { get; set; }
        public int Volume { get; set; }
        public int RemainderVolume { get; set; }
        public CellStatus Status { get; set; }
    }
}

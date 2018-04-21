using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class WarehouseCellSimpleViewModel
    {
        public int Id { get; set; }
        public string CellName { get; set; }
        public CellStatus Status { get; set; }
    }
}

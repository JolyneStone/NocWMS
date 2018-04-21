using System;
using System.ComponentModel.DataAnnotations;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class WarehouseCellFormViewModel
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        [Required(ErrorMessage = "库位不能为空")]
        [MaxLength(30, ErrorMessage = "库位长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "库位长度不能小于2个字符")]
        public string CellName { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "容量超出范围")]
        public int Volume { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "剩余容量超出范围")]
        public int RemainderVolume { get; set; }
        public CellStatus Status { get; set; }
    }
}

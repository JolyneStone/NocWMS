using System;
using System.ComponentModel.DataAnnotations;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class InventoryCellFormViewModel
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int WarehouseCellId { get; set; }
        public ReceiptType ReceiptType { get; set; }
        [Required(ErrorMessage = "单号不能为空")]
        [MaxLength(30, ErrorMessage = "单号长度不能大于30个字符")]
        public string ReceiptId { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "数量超出范围")]
        public int Num { get; set; }
    }
}

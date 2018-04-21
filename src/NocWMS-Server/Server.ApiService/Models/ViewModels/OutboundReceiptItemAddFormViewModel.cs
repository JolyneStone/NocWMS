using System;
using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class OutboundReceiptItemAddFormViewModel
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int WarehouseCellId { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "数量超出范围")]
        public int Num { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "出货价超出范围")]
        public float Price { get; set; }
    }
}

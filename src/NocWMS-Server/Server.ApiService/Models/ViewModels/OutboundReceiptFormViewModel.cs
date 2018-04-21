using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class OutboundReceiptFormViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "运单号不能为空")]
        [MaxLength(20, ErrorMessage = "运单号长度不能大于20个字符")]
        [MinLength(2, ErrorMessage = "运单号长度不能小于2个字符")]
        public string WaybillNo { get; set; }
        public string CustomerName { get; set; }
        public int WarehouseId { get; set; }
        [Required(ErrorMessage = "验收人不能为空")]
        [MaxLength(50, ErrorMessage = "验收人长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "验收人长度不能小于2个字符")]
        public string Acceptor { get; set; }
        [Required(ErrorMessage = "送货人长度不能为空")]
        [MaxLength(50, ErrorMessage = "送货人长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "送货人长度不能小于2个字符")]
        public string Deliveryman { get; set; }
        public bool IsDone { get; set; } = false;

        [Required(ErrorMessage = "出库单项目不可为空")]
        public OutboundReceiptItemAddFormViewModel[] OutboundReceiptItems { get; set; }
    }
}

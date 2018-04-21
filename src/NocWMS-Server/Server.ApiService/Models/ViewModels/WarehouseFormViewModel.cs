using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class WarehouseFormViewModel
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
        [Required(ErrorMessage = "仓库名称不能为空")]
        [MaxLength(50, ErrorMessage = "仓库名称长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "仓库名称长度不能小于2个字符")]
        public string WarehouseName { get; set; }
        [Required(ErrorMessage = "管理员名称不能为空")]
        [MaxLength(50, ErrorMessage = "管理员名称长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "管理员名称长度不能小于2个字符")]
        public string ManagerName { get; set; }
        [Required(ErrorMessage = "省份不能为空")]
        [MaxLength(20, ErrorMessage = "省份长度不能大于20个字符")]
        [MinLength(2, ErrorMessage = "省份长度不能小于2个字符")]
        public string Province { get; set; }
        [Required(ErrorMessage = "供应商地址不能为空")]
        [MaxLength(100, ErrorMessage = "供应商地址不能大于100个字符")]
        [MinLength(2, ErrorMessage = "供应商地址不能小于2个字符")]
        public string Address { get; set; }
        [MaxLength(255, ErrorMessage = "备注长度不能大于255个字符")]
        public string Remarks { get; set; }
        public IList<WarehouseCellFormViewModel> Cells { get; set; }
    }
}

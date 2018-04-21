using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "产品名称不能为空")]
        [MaxLength(50, ErrorMessage = "产品名称长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "产品名称长度不能小于2个字符")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "单位不能为空")]
        [MaxLength(10, ErrorMessage = "单位长度不能大于10个字符")]
        [MinLength(1, ErrorMessage = "单位长度不能小于1个字符")]
        public string Unit { get; set; }
        [Required(ErrorMessage = "规格不能为空")]
        [MaxLength(50, ErrorMessage = "规格长度不能大于50个字符")]
        [MinLength(1, ErrorMessage = "规格长度不能小于2个字符")]
        public string Spec { get; set; }
        [Required(ErrorMessage = "型号不能为空")]
        [MaxLength(50, ErrorMessage = "型号长度不能大于50个字符")]
        [MinLength(1, ErrorMessage = "型号长度不能小于1个字符")]
        public string Model { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "价格不能超出范围")]
        public float SellPrice { get; set; }
    }
}

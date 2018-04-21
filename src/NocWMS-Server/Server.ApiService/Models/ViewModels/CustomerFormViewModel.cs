using System.ComponentModel.DataAnnotations;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class CustomerFormViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "客户名称不能为空")]
        [MaxLength(50, ErrorMessage = "客户名称长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "客户名称长度不能小于2个字符")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "客户地址不能为空")]
        [MaxLength(100, ErrorMessage = "客户地址不能大于100个字符")]
        [MinLength(2, ErrorMessage = "客户地址不能小于2个字符")]
        public string Address { get; set; }
        [Required(ErrorMessage = "姓名长度不能为空")]
        [MaxLength(50, ErrorMessage = "姓名长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "姓名长度不能小于2个字符")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "性别不能为空")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "职务描述不能为空")]
        [MaxLength(50, ErrorMessage = "职务描述长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "职务描述长度不能小于2个字符")]
        public string Duty { get; set; }
        [MaxLength(50, ErrorMessage = "邮编长度不能大于50个字符")]
        [RegularExpression(@"[1-9]\d{5}(?!\d)", ErrorMessage = "请填写正确的邮编")]
        public string PostCode { get; set; }
        [MaxLength(11, ErrorMessage = "传真长度不能大于11个字符")]
        [RegularExpression(@"^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "手机号码不可为空")]
        [MaxLength(11, ErrorMessage = "手机号码长度不能大于11个字符")]
        [Phone(ErrorMessage = "请填写正确的手机号码")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "电子邮箱不能为空")]
        [MaxLength(50, ErrorMessage = "电子邮箱长度不能大于50个字符")]
        [EmailAddress(ErrorMessage = "请填写正确的电子邮箱地址")]
        public string Email { get; set; }
        [MaxLength(255, ErrorMessage = "备注长度不能大于255个字符")]
        public string Remarks { get; set; }
    }
}

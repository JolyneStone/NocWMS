using System.ComponentModel.DataAnnotations;
using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class StaffFormViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "姓名长度不能为空")]
        [MaxLength(50,ErrorMessage = "姓名长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "姓名长度不能小于2个字符")]
        public string StaffName { get; set; }
        [Required(ErrorMessage = "性别不能为空")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "职务描述不能为空")]
        [MaxLength(50, ErrorMessage = "职务描述长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "职务描述长度不能小于2个字符")]
        public string Duty { get; set; }
        [Required(ErrorMessage = "手机号码不可为空")]
        [MaxLength(11, ErrorMessage = "手机号码长度不能大于11个字符")]
        [Phone(ErrorMessage = "请填写正确的手机号码")]
        public string Telephone { get; set; }
        [MaxLength(16, ErrorMessage = "QQ号码长度不能大于16个字符")]
        [RegularExpression("[1-9][0-9]{4,14}", ErrorMessage = "请填写正确的QQ号码")]
        public string QQNumber { get; set; }
        [MaxLength(255, ErrorMessage = "备注长度不能大于255个字符")]
        public string Remarks { get; set; }
    }
}

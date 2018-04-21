using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "类别名称不可为空")]
        [MaxLength(30, ErrorMessage = "类别名称长度不能大于30个字符")]
        [MinLength(2, ErrorMessage = "类别名称长度不能小于2个字符")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "创建者名称不可为空")]
        [MaxLength(50, ErrorMessage = "创建者名称长度不能大于50个字符")]
        [MinLength(2, ErrorMessage = "创建者名称长度不能小于2个字符")]
        public string Creator { get; set; }
        [MaxLength(255, ErrorMessage = "备注长度不能大于255个字符")]
        public string Remarks { get; set; }
    }
}

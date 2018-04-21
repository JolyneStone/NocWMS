using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class StaffDisplayViewModel
    {
        public string Id { get; set; }
        public string StaffName { get; set; }
        public Gender Gender { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Duty { get; set; }
    }
}

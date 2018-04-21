using Server.ApiService.Common;

namespace Server.ApiService.Models.ViewModels
{
    public class CustomerDisplayViewModel
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Gender Gender { get; set; }
        public string Duty { get; set; }
        public string Telephone { get; set; }
    }
}

using System;

namespace Server.ApiService.Models.ViewModels
{
    public class CategoryDisplayViewModel
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string Creator { get; set; }
        public string Remarks { get; set; }
    }
}

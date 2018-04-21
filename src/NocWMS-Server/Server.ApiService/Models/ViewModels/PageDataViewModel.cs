using System.Collections.Generic;

namespace Server.ApiService.Models.ViewModels
{
    public class PageDataViewModel<T>
    {
        public int Total { get; set; }
        public IList<T> Data { get; set; }
    }
}

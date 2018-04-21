namespace Server.ApiService.Models.ViewModels
{
    public class ProductDisplayViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string Model { get; set; }
        public float SellPrice { get; set; }
    }
}

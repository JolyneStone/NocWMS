namespace Server.ApiService.Models
{
    public class VendorProduct
    {
        public string VendorId { get; set; }
        public int ProductId { get; set; }

        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
    }
}

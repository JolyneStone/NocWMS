namespace Server.ApiService.Models.ViewModels
{
    public class InboundReceiptItemsUpdateFormViewModel
    {
        /// <summary>
        /// InboundReceiptId
        /// </summary>
        public string Id { get; set; }
        public InboundReceiptItemAddFormViewModel[] AddItems { get; set; }
        public string[] RemoveItems { get; set; }
    }
}

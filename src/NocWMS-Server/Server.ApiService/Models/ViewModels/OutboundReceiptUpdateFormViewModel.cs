namespace Server.ApiService.Models.ViewModels
{
    public class OutboundReceiptUpdateFormViewModel
    {
        /// <summary>
        /// OutboundReceiptId
        /// </summary>
        public string Id { get; set; }
        public OutboundReceiptItemAddFormViewModel[] AddItems { get; set; }
        public string[] RemoveItems { get; set; }
    }
}

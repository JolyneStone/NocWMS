using System;
using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class InventoryAddFormViewModel
    {
        public int Id { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "账面库存不能范围")]
        public int BookInventory { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "实际库存超出范围")]
        public int RealInventory { get; set; }
    }
}

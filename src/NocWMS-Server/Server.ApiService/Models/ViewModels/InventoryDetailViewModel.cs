using System;
using System.ComponentModel.DataAnnotations;

namespace Server.ApiService.Models.ViewModels
{
    public class InventoryDetailViewModel
    {
        public int Id { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "账面库存不能范围")]
        public int BookInventory { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "实际库存超出范围")]
        public int RealInventory { get; set; }
        public string CategoryName { get; set; }
        public string WarehouseName { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public string ProductSpec { get; set; }
        public string ProductModel { get; set; }
        public string ProductPrice { get; set; }

        public InventoryCellDisplayViewModel[] InventoryCells { get; set; }
    }
}   

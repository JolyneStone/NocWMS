using Microsoft.EntityFrameworkCore;
using Server.ApiService.EntityFrameConfigurations;
using Server.ApiService.Models;

namespace Server.ApiService.Common
{
    public class NocDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InboundReceipt> InboundReceipts { get; set; }
        public DbSet<InboundReceiptItem> InboundReceiptItems { get; set; }
        public DbSet<OutboundReceipt> OutboundReceipts { get; set; }
        public DbSet<OutboundReceiptItem> OutboundReceiptItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseCell> WarehouseCells { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryCell> InventoryCells { get; set; }
        public DbSet<VendorProduct> VendorProducts { get; set; }

        public NocDbContext(DbContextOptions<NocDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new InboundReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new InboundReceiptItemConfiguration());
            modelBuilder.ApplyConfiguration(new OutboundReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new OutboundReceiptItemConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseCellConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new VendorProductConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryCellConfiguration());
        }
    }
}

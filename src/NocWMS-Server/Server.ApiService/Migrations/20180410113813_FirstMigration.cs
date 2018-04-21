using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(maxLength: 30, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Creator = table.Column<string>(maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Contact = table.Column<string>(maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: false),
                    Duty = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Fax = table.Column<string>(maxLength: 11, nullable: true),
                    Gender = table.Column<byte>(type: "TINYINT", nullable: false, defaultValueSql: "0"),
                    PostCode = table.Column<string>(maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 100, nullable: false),
                    Role = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Contact = table.Column<string>(maxLength: 50, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Duty = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Fax = table.Column<string>(maxLength: 11, nullable: true),
                    Gender = table.Column<byte>(type: "TINYINT", nullable: false, defaultValueSql: "0"),
                    PostCode = table.Column<string>(maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(maxLength: 11, nullable: false),
                    VendorName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    SellPrice = table.Column<float>(nullable: false),
                    Spec = table.Column<string>(maxLength: 50, nullable: false),
                    Unit = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Duty = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<byte>(type: "TINYINT", nullable: false, defaultValueSql: "0"),
                    QQNumber = table.Column<string>(maxLength: 16, nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: true),
                    StaffName = table.Column<string>(maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(maxLength: 11, nullable: false),
                    UserInfoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_UserInfos_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InboundReceiptItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Num = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProducingLocation = table.Column<string>(maxLength: 100, nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    StoreCell = table.Column<string>(maxLength: 100, nullable: false),
                    VendorName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundReceiptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InboundReceiptItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboundReceiptItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookInventory = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CostPrice = table.Column<float>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    RealInventory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutboundReceiptItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Num = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProducingLocation = table.Column<string>(maxLength: 100, nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    StoreCell = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundReceiptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutboundReceiptItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutboundReceiptItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendor_Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    VendorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor_Products", x => new { x.ProductId, x.VendorId });
                    table.ForeignKey(
                        name: "FK_Vendor_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_Products_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Remarks = table.Column<string>(maxLength: 255, nullable: true),
                    StaffId = table.Column<string>(nullable: true),
                    WarehouseName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InboundReceipts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Acceptor = table.Column<string>(maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Deliveryman = table.Column<string>(maxLength: 50, nullable: true),
                    HandlerName = table.Column<string>(maxLength: 50, nullable: true),
                    IsDone = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    StaffId = table.Column<string>(nullable: true),
                    Total = table.Column<float>(nullable: false),
                    VendorId = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: false),
                    WaybillNo = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InboundReceipts_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboundReceipts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InboundReceipts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OutboundReceipts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Acceptor = table.Column<string>(maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    CustomerId = table.Column<string>(nullable: true),
                    Deliveryman = table.Column<string>(maxLength: 50, nullable: true),
                    HandlerName = table.Column<string>(maxLength: 50, nullable: true),
                    IsDone = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    StaffId = table.Column<string>(nullable: true),
                    Total = table.Column<float>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    WaybillNo = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboundReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutboundReceipts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutboundReceipts_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OutboundReceipts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseCells",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CellName = table.Column<string>(maxLength: 30, nullable: false),
                    RemainderVolume = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    Status = table.Column<byte>(type: "TINYINT", nullable: false, defaultValueSql: "0"),
                    Volume = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseCells_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory_WarehouseCells",
                columns: table => new
                {
                    InventoryId = table.Column<int>(nullable: false),
                    WarehouseCellId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory_WarehouseCells", x => new { x.InventoryId, x.WarehouseCellId });
                    table.ForeignKey(
                        name: "FK_Inventory_WarehouseCells_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_WarehouseCells_WarehouseCells_WarehouseCellId",
                        column: x => x.WarehouseCellId,
                        principalTable: "WarehouseCells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceiptItems_CategoryId",
                table: "InboundReceiptItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceiptItems_ProductId",
                table: "InboundReceiptItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceipts_StaffId",
                table: "InboundReceipts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceipts_VendorId",
                table: "InboundReceipts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceipts_WarehouseId",
                table: "InboundReceipts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CategoryId",
                table: "Inventories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_WarehouseCells_WarehouseCellId",
                table: "Inventory_WarehouseCells",
                column: "WarehouseCellId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceiptItems_CategoryId",
                table: "OutboundReceiptItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceiptItems_ProductId",
                table: "OutboundReceiptItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceipts_CustomerId",
                table: "OutboundReceipts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceipts_StaffId",
                table: "OutboundReceipts",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceipts_WarehouseId",
                table: "OutboundReceipts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserInfoId",
                table: "Staffs",
                column: "UserInfoId",
                unique: true,
                filter: "[UserInfoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Products_VendorId",
                table: "Vendor_Products",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseCells_WarehouseId",
                table: "WarehouseCells",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_StaffId",
                table: "Warehouses",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboundReceiptItems");

            migrationBuilder.DropTable(
                name: "InboundReceipts");

            migrationBuilder.DropTable(
                name: "Inventory_WarehouseCells");

            migrationBuilder.DropTable(
                name: "OutboundReceiptItems");

            migrationBuilder.DropTable(
                name: "OutboundReceipts");

            migrationBuilder.DropTable(
                name: "Vendor_Products");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "WarehouseCells");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}

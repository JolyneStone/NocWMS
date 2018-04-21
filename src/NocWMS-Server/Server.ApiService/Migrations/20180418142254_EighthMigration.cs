using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class EighthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory_WarehouseCells");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "OutboundReceipts",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Inventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "InboundReceipts",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(float));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Warehouses_WarehouseName",
                table: "Warehouses",
                column: "WarehouseName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Vendors_VendorName",
                table: "Vendors",
                column: "VendorName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Products_ProductName",
                table: "Products",
                column: "ProductName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Customers_CustomerName",
                table: "Customers",
                column: "CustomerName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateTable(
                name: "Inventory_Cells",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    InventoryId = table.Column<int>(nullable: false),
                    Num = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: true),
                    ReceiptId = table.Column<string>(maxLength: 30, nullable: false),
                    ReceiptType = table.Column<byte>(type: "TINYINT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    WarehouseCellId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory_Cells", x => new { x.Id, x.InventoryId });
                    table.ForeignKey(
                        name: "FK_Inventory_Cells_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Cells_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Cells_WarehouseCells_WarehouseCellId",
                        column: x => x.WarehouseCellId,
                        principalTable: "WarehouseCells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Cells_InventoryId",
                table: "Inventory_Cells",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Cells_ProductId",
                table: "Inventory_Cells",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Cells_WarehouseCellId",
                table: "Inventory_Cells",
                column: "WarehouseCellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                table: "Inventories",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Warehouses_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropTable(
                name: "Inventory_Cells");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Warehouses_WarehouseName",
                table: "Warehouses");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Vendors_VendorName",
                table: "Vendors");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Products_ProductName",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_WarehouseId",
                table: "Inventories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Customers_CustomerName",
                table: "Customers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Inventories");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "OutboundReceipts",
                nullable: false,
                oldClrType: typeof(float),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "InboundReceipts",
                nullable: false,
                oldClrType: typeof(float),
                oldDefaultValueSql: "0");

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
                name: "IX_Inventory_WarehouseCells_WarehouseCellId",
                table: "Inventory_WarehouseCells",
                column: "WarehouseCellId");
        }
    }
}

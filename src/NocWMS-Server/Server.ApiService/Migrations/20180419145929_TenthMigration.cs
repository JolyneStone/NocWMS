using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class TenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "OutboundReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "InboundReceiptItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceiptItems_InventoryId",
                table: "OutboundReceiptItems",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceiptItems_InventoryId",
                table: "InboundReceiptItems",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboundReceiptItems_Inventories_InventoryId",
                table: "InboundReceiptItems",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutboundReceiptItems_Inventories_InventoryId",
                table: "OutboundReceiptItems",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboundReceiptItems_Inventories_InventoryId",
                table: "InboundReceiptItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutboundReceiptItems_Inventories_InventoryId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_OutboundReceiptItems_InventoryId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_InboundReceiptItems_InventoryId",
                table: "InboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "InboundReceiptItems");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreCell",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "StoreCell",
                table: "InboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "InboundReceiptItems");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseCellId",
                table: "OutboundReceiptItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseCellId",
                table: "InboundReceiptItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceiptItems_WarehouseCellId",
                table: "OutboundReceiptItems",
                column: "WarehouseCellId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceiptItems_WarehouseCellId",
                table: "InboundReceiptItems",
                column: "WarehouseCellId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboundReceiptItems_WarehouseCells_WarehouseCellId",
                table: "InboundReceiptItems",
                column: "WarehouseCellId",
                principalTable: "WarehouseCells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutboundReceiptItems_WarehouseCells_WarehouseCellId",
                table: "OutboundReceiptItems",
                column: "WarehouseCellId",
                principalTable: "WarehouseCells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboundReceiptItems_WarehouseCells_WarehouseCellId",
                table: "InboundReceiptItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutboundReceiptItems_WarehouseCells_WarehouseCellId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_OutboundReceiptItems_WarehouseCellId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_InboundReceiptItems_WarehouseCellId",
                table: "InboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "WarehouseCellId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "WarehouseCellId",
                table: "InboundReceiptItems");

            migrationBuilder.AddColumn<string>(
                name: "StoreCell",
                table: "OutboundReceiptItems",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "CostPrice",
                table: "Inventories",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "StoreCell",
                table: "InboundReceiptItems",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "InboundReceiptItems",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

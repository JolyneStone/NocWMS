using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class EleventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Cells_WarehouseCells_WarehouseCellId",
                table: "Inventory_Cells");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_Cells_WarehouseCellId",
                table: "Inventory_Cells");

            migrationBuilder.DropColumn(
                name: "WarehouseCellId",
                table: "Inventory_Cells");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Cells_WarehouseCells_Id",
                table: "Inventory_Cells",
                column: "Id",
                principalTable: "WarehouseCells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Cells_WarehouseCells_Id",
                table: "Inventory_Cells");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseCellId",
                table: "Inventory_Cells",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Cells_WarehouseCellId",
                table: "Inventory_Cells",
                column: "WarehouseCellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Cells_WarehouseCells_WarehouseCellId",
                table: "Inventory_Cells",
                column: "WarehouseCellId",
                principalTable: "WarehouseCells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

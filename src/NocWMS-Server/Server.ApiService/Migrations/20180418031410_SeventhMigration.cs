using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Server.ApiService.Migrations
{
    public partial class SeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "ProducingLocation",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "InboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "ProducingLocation",
                table: "InboundReceiptItems");

            migrationBuilder.AddColumn<string>(
                name: "OutboundReceiptId",
                table: "OutboundReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InboundReceiptId",
                table: "InboundReceiptItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboundReceiptItems_OutboundReceiptId",
                table: "OutboundReceiptItems",
                column: "OutboundReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceiptItems_InboundReceiptId",
                table: "InboundReceiptItems",
                column: "InboundReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboundReceiptItems_InboundReceipts_InboundReceiptId",
                table: "InboundReceiptItems",
                column: "InboundReceiptId",
                principalTable: "InboundReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OutboundReceiptItems_OutboundReceipts_OutboundReceiptId",
                table: "OutboundReceiptItems",
                column: "OutboundReceiptId",
                principalTable: "OutboundReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboundReceiptItems_InboundReceipts_InboundReceiptId",
                table: "InboundReceiptItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OutboundReceiptItems_OutboundReceipts_OutboundReceiptId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_OutboundReceiptItems_OutboundReceiptId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropIndex(
                name: "IX_InboundReceiptItems_InboundReceiptId",
                table: "InboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "OutboundReceiptId",
                table: "OutboundReceiptItems");

            migrationBuilder.DropColumn(
                name: "InboundReceiptId",
                table: "InboundReceiptItems");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "OutboundReceiptItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProducingLocation",
                table: "OutboundReceiptItems",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "InboundReceiptItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProducingLocation",
                table: "InboundReceiptItems",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

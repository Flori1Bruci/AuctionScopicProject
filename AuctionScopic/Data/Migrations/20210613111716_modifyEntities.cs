using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionScopic.Data.Migrations
{
    public partial class modifyEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncreaseAmount",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "RequiredIncreaseAmount",
                table: "Items",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "SoldTo",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredIncreaseAmount",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SoldTo",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseAmount",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

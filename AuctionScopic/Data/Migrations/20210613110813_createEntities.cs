using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionScopic.Data.Migrations
{
    public partial class createEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WalletAmount",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InitialPrice = table.Column<decimal>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    AvailableForAuction = table.Column<bool>(nullable: false),
                    FinishAuctionTime = table.Column<DateTime>(nullable: true),
                    IncreaseAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoBids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IsActivated = table.Column<bool>(nullable: false),
                    IncreaseAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoBids_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    BidAmount = table.Column<decimal>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoBids_ItemId",
                table: "AutoBids",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ItemId",
                table: "Bids",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoBids");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropColumn(
                name: "WalletAmount",
                table: "AspNetUsers");
        }
    }
}

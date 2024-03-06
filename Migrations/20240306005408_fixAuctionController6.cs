using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionBackend.Migrations
{
    /// <inheritdoc />
    public partial class fixAuctionController6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_UserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Auctions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ApplicationUserId",
                table: "Auctions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_ApplicationUserId",
                table: "Auctions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_ApplicationUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ApplicationUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_UserId",
                table: "Auctions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

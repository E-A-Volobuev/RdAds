using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RnAds.DAL.Migrations
{
    public partial class EditMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_MessageId",
                table: "Pictures",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Messages_MessageId",
                table: "Pictures",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Messages_MessageId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_MessageId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Pictures");
        }
    }
}

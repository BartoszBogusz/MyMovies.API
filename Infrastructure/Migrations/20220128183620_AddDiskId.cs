using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDiskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiskId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DiskId",
                table: "Movies",
                column: "DiskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Disks_DiskId",
                table: "Movies",
                column: "DiskId",
                principalTable: "Disks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Disks_DiskId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DiskId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DiskId",
                table: "Movies");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class Addedparentandchildrealtions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ParentId",
                table: "Posts",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_ParentId",
                table: "Posts",
                column: "ParentId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_ParentId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ParentId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Posts");
        }
    }
}

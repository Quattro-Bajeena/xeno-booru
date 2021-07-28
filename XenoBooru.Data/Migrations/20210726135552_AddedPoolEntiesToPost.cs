using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class AddedPoolEntiesToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Posts_PostEntityId",
                table: "Pools");

            migrationBuilder.DropIndex(
                name: "IX_Pools_PostEntityId",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "PostEntityId",
                table: "Pools");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostEntityId",
                table: "Pools",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pools_PostEntityId",
                table: "Pools",
                column: "PostEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Posts_PostEntityId",
                table: "Pools",
                column: "PostEntityId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

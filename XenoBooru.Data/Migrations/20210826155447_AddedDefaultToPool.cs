using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class AddedDefaultToPool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Pools",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Pools");
        }
    }
}

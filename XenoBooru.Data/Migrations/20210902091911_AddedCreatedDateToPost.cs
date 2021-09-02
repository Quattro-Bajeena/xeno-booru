using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class AddedCreatedDateToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                //defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Posts");
        }
    }
}

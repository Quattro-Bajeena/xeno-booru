using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class changesourceDownloadtoFileNameDownoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceDownload",
                table: "Posts",
                newName: "FileNameDownload");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileNameDownload",
                table: "Posts",
                newName: "SourceDownload");
        }
    }
}

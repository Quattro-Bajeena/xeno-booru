using Microsoft.EntityFrameworkCore.Migrations;

namespace XenoBooru.Data.Migrations
{
    public partial class AddedPoolEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolEntityPostEntity");

            migrationBuilder.AddColumn<int>(
                name: "PostEntityId",
                table: "Pools",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PoolEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    PoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoolEntries_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoolEntries_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pools_PostEntityId",
                table: "Pools",
                column: "PostEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolEntries_PoolId",
                table: "PoolEntries",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolEntries_PostId",
                table: "PoolEntries",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pools_Posts_PostEntityId",
                table: "Pools",
                column: "PostEntityId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pools_Posts_PostEntityId",
                table: "Pools");

            migrationBuilder.DropTable(
                name: "PoolEntries");

            migrationBuilder.DropIndex(
                name: "IX_Pools_PostEntityId",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "PostEntityId",
                table: "Pools");

            migrationBuilder.CreateTable(
                name: "PoolEntityPostEntity",
                columns: table => new
                {
                    PoolsId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolEntityPostEntity", x => new { x.PoolsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PoolEntityPostEntity_Pools_PoolsId",
                        column: x => x.PoolsId,
                        principalTable: "Pools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoolEntityPostEntity_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolEntityPostEntity_PostsId",
                table: "PoolEntityPostEntity",
                column: "PostsId");
        }
    }
}

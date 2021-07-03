using Microsoft.EntityFrameworkCore.Migrations;

namespace CWWebApi.Migrations
{
    public partial class AuthReworked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenInfo");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ApiUsers");

            migrationBuilder.CreateTable(
                name: "ApiUserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiUserRoles_ApiUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApiUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiUserRoles_UserId",
                table: "ApiUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUserRoles");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "ApiUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TokenInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenInfo_ApiUsers_Id",
                        column: x => x.Id,
                        principalTable: "ApiUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }
    }
}

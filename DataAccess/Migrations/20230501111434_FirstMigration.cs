using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_CompleteMigrations",
                columns: table => new
                {
                    CompleteMigrationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CompleteMigrations", x => x.CompleteMigrationId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    IdUserInfo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.IdUserInfo);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    IdLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdUserInfo = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Target = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ObjectTabke = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.IdLog);
                    table.ForeignKey(
                        name: "FK_Logs_UserInfos_UserIdUserInfo",
                        column: x => x.UserIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_UserInfos_IdUser",
                        column: x => x.IdUser,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserIdUserInfo",
                table: "Logs",
                column: "UserIdUserInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_CompleteMigrations");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}

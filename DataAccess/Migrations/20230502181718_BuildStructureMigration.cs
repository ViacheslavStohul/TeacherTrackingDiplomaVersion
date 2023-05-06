using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BuildStructureMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UserInfos_UserIdUserInfo",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInfos_IdUser",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AccessLevelId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChairId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComissionId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RankId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTypeId",
                table: "UserInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccessLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chair = table.Column<bool>(type: "bit", nullable: false),
                    Comission = table.Column<bool>(type: "bit", nullable: false),
                    Departament = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chairs",
                columns: table => new
                {
                    ChairId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chairs", x => x.ChairId);
                });

            migrationBuilder.CreateTable(
                name: "Methodicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInfoIdUserInfo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methodicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methodicals_UserInfos_UserInfoIdUserInfo",
                        column: x => x.UserInfoIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInfoIdUserInfo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalWorks_UserInfos_UserInfoIdUserInfo",
                        column: x => x.UserInfoIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserInfoIdUserInfo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_Qualifications_UserInfos_UserInfoIdUserInfo",
                        column: x => x.UserInfoIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChairHeads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    HeadIdUserInfo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChairHeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChairHeads_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "ChairId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChairHeads_UserInfos_HeadIdUserInfo",
                        column: x => x.HeadIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    DepartamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviatoin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    HeadIdUserInfo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departaments", x => x.DepartamentId);
                    table.ForeignKey(
                        name: "FK_Departaments_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "ChairId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departaments_UserInfos_HeadIdUserInfo",
                        column: x => x.HeadIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    ComissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.ComissionId);
                    table.ForeignKey(
                        name: "FK_Comissions_Departaments_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departaments",
                        principalColumn: "DepartamentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComissionHeads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComissionId = table.Column<int>(type: "int", nullable: false),
                    HeadIdUserInfo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissionHeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComissionHeads_Comissions_ComissionId",
                        column: x => x.ComissionId,
                        principalTable: "Comissions",
                        principalColumn: "ComissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComissionHeads_UserInfos_HeadIdUserInfo",
                        column: x => x.HeadIdUserInfo,
                        principalTable: "UserInfos",
                        principalColumn: "IdUserInfo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_AccessLevelId",
                table: "UserInfos",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_CategoryId",
                table: "UserInfos",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ChairId",
                table: "UserInfos",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ComissionId",
                table: "UserInfos",
                column: "ComissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RankId",
                table: "UserInfos",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_WorkTypeId",
                table: "UserInfos",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChairHeads_ChairId",
                table: "ChairHeads",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_ChairHeads_HeadIdUserInfo",
                table: "ChairHeads",
                column: "HeadIdUserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_ComissionHeads_ComissionId",
                table: "ComissionHeads",
                column: "ComissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissionHeads_HeadIdUserInfo",
                table: "ComissionHeads",
                column: "HeadIdUserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Comissions_DepartamentId",
                table: "Comissions",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_ChairId",
                table: "Departaments",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_HeadIdUserInfo",
                table: "Departaments",
                column: "HeadIdUserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Methodicals_UserInfoIdUserInfo",
                table: "Methodicals",
                column: "UserInfoIdUserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalWorks_UserInfoIdUserInfo",
                table: "OrganizationalWorks",
                column: "UserInfoIdUserInfo");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_UserInfoIdUserInfo",
                table: "Qualifications",
                column: "UserInfoIdUserInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UserInfos_UserIdUserInfo",
                table: "Logs",
                column: "UserIdUserInfo",
                principalTable: "UserInfos",
                principalColumn: "IdUserInfo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_AccessLevels_AccessLevelId",
                table: "UserInfos",
                column: "AccessLevelId",
                principalTable: "AccessLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Categories_CategoryId",
                table: "UserInfos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Chairs_ChairId",
                table: "UserInfos",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "ChairId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Comissions_ComissionId",
                table: "UserInfos",
                column: "ComissionId",
                principalTable: "Comissions",
                principalColumn: "ComissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Ranks_RankId",
                table: "UserInfos",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_WorkTypes_WorkTypeId",
                table: "UserInfos",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInfos_IdUser",
                table: "Users",
                column: "IdUser",
                principalTable: "UserInfos",
                principalColumn: "IdUserInfo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UserInfos_UserIdUserInfo",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_AccessLevels_AccessLevelId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Categories_CategoryId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Chairs_ChairId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Comissions_ComissionId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Ranks_RankId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_WorkTypes_WorkTypeId",
                table: "UserInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInfos_IdUser",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ChairHeads");

            migrationBuilder.DropTable(
                name: "ComissionHeads");

            migrationBuilder.DropTable(
                name: "Methodicals");

            migrationBuilder.DropTable(
                name: "OrganizationalWorks");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "Comissions");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.DropTable(
                name: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_AccessLevelId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_CategoryId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ChairId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_ComissionId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_RankId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_WorkTypeId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "ChairId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "ComissionId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "RankId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "WorkTypeId",
                table: "UserInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UserInfos_UserIdUserInfo",
                table: "Logs",
                column: "UserIdUserInfo",
                principalTable: "UserInfos",
                principalColumn: "IdUserInfo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInfos_IdUser",
                table: "Users",
                column: "IdUser",
                principalTable: "UserInfos",
                principalColumn: "IdUserInfo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddDatabaseStructureMigration : Migration
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
                name: "AccessLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User = table.Column<bool>(type: "bit", nullable: false),
                    Chair = table.Column<bool>(type: "bit", nullable: false),
                    Comission = table.Column<bool>(type: "bit", nullable: false),
                    Departament = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BanLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BanStarter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BanEnded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanLogs", x => x.Id);
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
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    ComissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.ComissionId);
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
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ChairId = table.Column<int>(type: "int", nullable: true),
                    ComissionId = table.Column<int>(type: "int", nullable: true),
                    WorkTypeId = table.Column<int>(type: "int", nullable: true),
                    AccessLevelId = table.Column<int>(type: "int", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.IdUserInfo);
                    table.ForeignKey(
                        name: "FK_UserInfos_AccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "AccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "ChairId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_Comissions_ComissionId",
                        column: x => x.ComissionId,
                        principalTable: "Comissions",
                        principalColumn: "ComissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInfos_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
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

            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    DepartamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviatoin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    HeadIdUserInfo = table.Column<int>(type: "int", nullable: true)
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
                name: "Logs",
                columns: table => new
                {
                    IdLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdUserInfo = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Target = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ObjectTable = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Methodicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Logs_UserIdUserInfo",
                table: "Logs",
                column: "UserIdUserInfo");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChairHeads_UserInfos_HeadIdUserInfo",
                table: "ChairHeads",
                column: "HeadIdUserInfo",
                principalTable: "UserInfos",
                principalColumn: "IdUserInfo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comissions_Departaments_DepartamentId",
                table: "Comissions",
                column: "DepartamentId",
                principalTable: "Departaments",
                principalColumn: "DepartamentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departaments_UserInfos_HeadIdUserInfo",
                table: "Departaments");

            migrationBuilder.DropTable(
                name: "_CompleteMigrations");

            migrationBuilder.DropTable(
                name: "BanLogs");

            migrationBuilder.DropTable(
                name: "ChairHeads");

            migrationBuilder.DropTable(
                name: "ComissionHeads");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Methodicals");

            migrationBuilder.DropTable(
                name: "OrganizationalWorks");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comissions");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "Departaments");

            migrationBuilder.DropTable(
                name: "Chairs");
        }
    }
}

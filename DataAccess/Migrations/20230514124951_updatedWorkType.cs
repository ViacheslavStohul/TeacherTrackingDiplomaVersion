using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updatedWorkType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizationType",
                table: "OrganizationalWorks",
                newName: "OrganizationTypeId");

            migrationBuilder.CreateTable(
                name: "OrganizationalWorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalWorkTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalWorks_OrganizationTypeId",
                table: "OrganizationalWorks",
                column: "OrganizationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalWorks_OrganizationalWorkTypes_OrganizationTypeId",
                table: "OrganizationalWorks",
                column: "OrganizationTypeId",
                principalTable: "OrganizationalWorkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalWorks_OrganizationalWorkTypes_OrganizationTypeId",
                table: "OrganizationalWorks");

            migrationBuilder.DropTable(
                name: "OrganizationalWorkTypes");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalWorks_OrganizationTypeId",
                table: "OrganizationalWorks");

            migrationBuilder.RenameColumn(
                name: "OrganizationTypeId",
                table: "OrganizationalWorks",
                newName: "OrganizationType");
        }
    }
}

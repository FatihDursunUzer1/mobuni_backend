using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobUni.Infrastructure.Migrations
{
    public partial class userForeignKeyTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UniversityId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "FK_Users_Universities_UniversityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FK_Users_Universities_UniversityId",
                table: "Users",
                column: "FK_Users_Universities_UniversityId",
                unique: true,
                filter: "[FK_Users_Universities_UniversityId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Universities_FK_Users_Universities_UniversityId",
                table: "Users",
                column: "FK_Users_Universities_UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Universities_FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniversityId",
                table: "Users",
                column: "UniversityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }
    }
}

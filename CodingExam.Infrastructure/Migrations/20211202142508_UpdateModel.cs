using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingExam.Infrastructure.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestDetails_Interests_InterestId",
                table: "InterestDetails");

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "InterestDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InterestDetails_Interests_InterestId",
                table: "InterestDetails",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestDetails_Interests_InterestId",
                table: "InterestDetails");

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "InterestDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestDetails_Interests_InterestId",
                table: "InterestDetails",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

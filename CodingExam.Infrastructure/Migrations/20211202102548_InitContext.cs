using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingExam.Infrastructure.Migrations
{
    public partial class InitContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentValue = table.Column<double>(type: "float", nullable: false),
                    LowerBoundInterestRate = table.Column<double>(type: "float", nullable: false),
                    UpperBoundInterestRate = table.Column<double>(type: "float", nullable: false),
                    IncrementalRate = table.Column<double>(type: "float", nullable: false),
                    MaturityYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PresentValue = table.Column<double>(type: "float", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    FutureValue = table.Column<double>(type: "float", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestDetails_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestDetails_InterestId",
                table: "InterestDetails",
                column: "InterestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestDetails");

            migrationBuilder.DropTable(
                name: "Interests");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class removedNullableProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "Quizs",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "Quizs",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}

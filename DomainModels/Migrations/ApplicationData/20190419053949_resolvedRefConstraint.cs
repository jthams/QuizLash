using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class resolvedRefConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionRelation_Quizs_QuizID",
                table: "QuizQuestionRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionRelation_Quizs_QuizID",
                table: "QuizQuestionRelation",
                column: "QuizID",
                principalTable: "Quizs",
                principalColumn: "QuizID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionRelation_Quizs_QuizID",
                table: "QuizQuestionRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionRelation_Quizs_QuizID",
                table: "QuizQuestionRelation",
                column: "QuizID",
                principalTable: "Quizs",
                principalColumn: "QuizID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

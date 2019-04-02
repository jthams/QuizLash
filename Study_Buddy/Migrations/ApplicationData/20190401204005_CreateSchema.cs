using Microsoft.EntityFrameworkCore.Migrations;

namespace Study_Buddy.Migrations.ApplicationData
{
    public partial class CreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Question_QuestionID",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz_QuizID",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Quiz",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Quiz",
                newName: "QuizID");

            migrationBuilder.RenameColumn(
                name: "QuizID",
                table: "Question",
                newName: "QuizRefID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Question",
                newName: "QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuizID",
                table: "Question",
                newName: "IX_Question_QuizRefID");

            migrationBuilder.RenameColumn(
                name: "QuestionID",
                table: "Choices",
                newName: "QuestionRefID");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_QuestionID",
                table: "Choices",
                newName: "IX_Choices_QuestionRefID");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Question_QuestionRefID",
                table: "Choices",
                column: "QuestionRefID",
                principalTable: "Question",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz_QuizRefID",
                table: "Question",
                column: "QuizRefID",
                principalTable: "Quiz",
                principalColumn: "QuizID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Question_QuestionRefID",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quiz_QuizRefID",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "Quiz",
                newName: "OwnerID");

            migrationBuilder.RenameColumn(
                name: "QuizID",
                table: "Quiz",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "QuizRefID",
                table: "Question",
                newName: "QuizID");

            migrationBuilder.RenameColumn(
                name: "QuestionID",
                table: "Question",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuizRefID",
                table: "Question",
                newName: "IX_Question_QuizID");

            migrationBuilder.RenameColumn(
                name: "QuestionRefID",
                table: "Choices",
                newName: "QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_QuestionRefID",
                table: "Choices",
                newName: "IX_Choices_QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Question_QuestionID",
                table: "Choices",
                column: "QuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quiz_QuizID",
                table: "Question",
                column: "QuizID",
                principalTable: "Quiz",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

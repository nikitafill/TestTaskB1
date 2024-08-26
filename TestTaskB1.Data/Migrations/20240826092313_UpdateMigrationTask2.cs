using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskB1.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationTask2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Debit",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomingBalance",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutgoingBalance",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ClassId",
                table: "Account",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ClassModel_ClassId",
                table: "Account",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ClassModel_ClassId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ClassId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "Debit",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "IncomingBalance",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "OutgoingBalance",
                table: "ClassModel");
        }
    }
}

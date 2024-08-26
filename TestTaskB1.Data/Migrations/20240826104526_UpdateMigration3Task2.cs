using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskB1.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration3Task2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IncomingPassive",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutgoingPassive",
                table: "ClassModel",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IncomingPassive",
                table: "Account",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OutgoingPassive",
                table: "Account",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomingPassive",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "OutgoingPassive",
                table: "ClassModel");

            migrationBuilder.DropColumn(
                name: "IncomingPassive",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "OutgoingPassive",
                table: "Account");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsuranceERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedmessagefromappusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Claims_ClaimId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_ClaimId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Claims");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "Claims",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ClaimId",
                table: "Expenses",
                column: "ClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Claims_ClaimId",
                table: "Expenses",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

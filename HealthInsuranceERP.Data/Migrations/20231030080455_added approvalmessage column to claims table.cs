using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsuranceERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedapprovalmessagecolumntoclaimstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovalMessage",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "ApprovalMessage",
                table: "Claims");
        }
    }
}

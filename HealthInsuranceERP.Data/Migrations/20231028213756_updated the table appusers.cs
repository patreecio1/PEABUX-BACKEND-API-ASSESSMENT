using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthInsuranceERP.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedthetableappusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOA",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DOASetBy",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DelegationId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "FunctionId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "AppUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DOA",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DOASetBy",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DelegationId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FunctionId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "UnitId",
                table: "AppUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitoringSys.DATA.Migrations
{
    public partial class EditNameUpdateToLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVehicleStatusUpdateId",
                table: "VehicleStatuses");

            migrationBuilder.AddColumn<int>(
                name: "LastVehicleStatusLogId",
                table: "VehicleStatuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastVehicleStatusLogId",
                table: "VehicleStatuses");

            migrationBuilder.AddColumn<int>(
                name: "LastVehicleStatusUpdateId",
                table: "VehicleStatuses",
                type: "int",
                nullable: true);
        }
    }
}

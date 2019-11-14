using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelLocker.DAL.Migrations
{
    public partial class reason_of_visit_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReasonOfVisit",
                table: "RoomAccesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonOfVisit",
                table: "RoomAccesses");
        }
    }
}

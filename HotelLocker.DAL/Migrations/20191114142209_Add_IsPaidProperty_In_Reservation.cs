using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelLocker.DAL.Migrations
{
    public partial class Add_IsPaidProperty_In_Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Reservations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Reservations");
        }
    }
}

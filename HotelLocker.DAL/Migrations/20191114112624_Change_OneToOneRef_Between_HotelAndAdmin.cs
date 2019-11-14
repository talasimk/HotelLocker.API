using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelLocker.DAL.Migrations
{
    public partial class Change_OneToOneRef_Between_HotelAndAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hotels_HotelStaff_HotelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HotelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HotelStaff_HotelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HotelStaff_HotelId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "HotelAdminId",
                table: "Hotels",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "HotelStaff_FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HotelStaff_LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelAdminId",
                table: "Hotels",
                column: "HotelAdminId",
                unique: true,
                filter: "[HotelAdminId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HotelId",
                table: "AspNetUsers",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_HotelAdminId",
                table: "Hotels",
                column: "HotelAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_HotelAdminId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelAdminId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HotelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HotelAdminId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelStaff_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HotelStaff_LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelStaff_HotelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HotelId",
                table: "AspNetUsers",
                column: "HotelId",
                unique: true,
                filter: "[HotelId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HotelStaff_HotelId",
                table: "AspNetUsers",
                column: "HotelStaff_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hotels_HotelStaff_HotelId",
                table: "AspNetUsers",
                column: "HotelStaff_HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

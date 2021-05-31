using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceServer.Migrations
{
    public partial class DeliveryDaysUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "Products",
                newName: "DeliveryDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryDays",
                table: "Products",
                newName: "DeliveryTime");
        }
    }
}

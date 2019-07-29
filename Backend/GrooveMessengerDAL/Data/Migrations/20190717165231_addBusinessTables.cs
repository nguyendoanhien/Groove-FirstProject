using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addBusinessTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DisplayName",
                "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                "DisplayName",
                "UserInfo",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DisplayName",
                "UserInfo");

            migrationBuilder.AddColumn<string>(
                "DisplayName",
                "AspNetUsers",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }
    }
}
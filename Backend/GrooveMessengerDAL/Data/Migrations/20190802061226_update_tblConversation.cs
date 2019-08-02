using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class update_tblConversation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Conversation",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Conversation",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}

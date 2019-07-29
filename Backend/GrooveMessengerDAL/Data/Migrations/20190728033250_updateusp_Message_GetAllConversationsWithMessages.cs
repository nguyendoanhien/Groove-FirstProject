using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class updateusp_Message_GetAllConversationsWithMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_rename 'dbo.msp_GetAllConversationsWithMessages', 'usp_Message_GetAllConversationsWithMessages';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

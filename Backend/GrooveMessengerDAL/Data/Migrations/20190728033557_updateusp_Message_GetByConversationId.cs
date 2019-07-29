using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class updateusp_Message_GetByConversationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"EXEC sp_rename 'dbo.csp_GetConversationById', 'usp_Message_GetByConversationId';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
